using PcapDotNet.Packets;
using PcapDotNet.Packets.Ethernet;
using PcapDotNet.Packets.IpV4;
using sw_router.Builder;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading;

namespace sw_router.Processing
{

    class ArpEntry
    {
        public IpV4Address ip { get; set; }
        public MacAddress mac { get; set; }
        public long time { get; set; }
        public int netInterface { get; set; }


        public ArpEntry(IpV4Address ip, MacAddress mac, long time, int netInterface)
        {
            this.ip = ip;
            this.mac = mac;
            this.time = time;
            this.netInterface = netInterface;
        }

        public override string ToString()
        {
            return ip.ToString() + " - " + mac.ToString() + " at " + netInterface.ToString();
        }
    }
    class Arp : Processing
    {
        /* SINGLETON START*/
        private static readonly Lazy<Arp> _instance = new Lazy<Arp>(() => new Arp());
        public static Arp Instance
        {
            get
            {
                return _instance.Value;
            }
        }
        /* SINGLETON END*/


        public List<ArpEntry> arpTable;
        public Thread checkOldArp_t;
        public Object arpCache_lock;
        public int arpCacheTimeout = 3;

        private Arp()
        {
            arpCache_lock = new Object();

            arpTable = new List<ArpEntry>();

            checkOldArp_t = new Thread(new ThreadStart(checkOldArp));
            checkOldArp_t.Start();
            //addArp(new IpV4Address(0)., new MacAddress("00:00:00:00:00:00"), DateTime.Now, 0);
        }

        // get mac address for given ip, send arp request if mac not in table
        public MacAddress get(IpV4Address ip, int netInterface)
        {
            ArpEntry entry = searchArpCache(ip);
            int max_rounds = 2;            
            do
            {
                // TODO: shall i wait for arp reply or just sed request and drop packet?
                entry = searchArpCache(ip);
                if (entry == null)
                {
                    Logger.log("Getting mac (via ARP) for ip: " + ip.ToString());
                    arping(netInterface, ip.ToString());
                    Thread.Sleep(500);
                }
                else
                {
                    Controller.Instance.gui.refresArpTable();
                    return entry.mac;
                }
            } while (max_rounds-- > 0);
            
            Logger.log("Host didnt respond to arping in time...");
            return new MacAddress("ff:ff:ff:ff:ff:ff");
        }
        

        public void checkOldArp()
        {
            // dont delete
            return;
            long value = 0;     
            while (true)
            {
                // Logger.log("Check arp for old");
                for (int i = arpTable.Count - 1; i >= 0; i--)
                {
                    ArpEntry entry = arpTable[i];

                    if ( long.TryParse(entry.time.ToString(), out value) ) 
                    {                  
                        if (value + arpCacheTimeout * 10000000 < DateTime.Now.Ticks)
                        {
                            arpTable.RemoveAt(i);
                            i--;
                            try
                            {
                                Controller.Instance.gui.refresArpTable();
                            }
                            catch (Exception e)
                            {
                            }
                        }
                            
                    }  
                }
                
                Thread.Sleep(500);
            }
        }
       
        public void flushArp()
        {
            arpTable.Clear();
            Controller.Instance.gui.refresArpTable();
        }

        public ArpEntry searchArpCache(IpV4Address ip)
        {

            if ( arpTable.Exists(ArpEntry => ArpEntry.ip == ip) )
            {
                return arpTable.Find(ArpEntry => ArpEntry.ip == ip);
            }
            return null;
        }

        public void addArp(IpV4Address ip, MacAddress mac, DateTime time, int netInterface)
        {
            if ( arpTable.Exists(ArpEntry => ArpEntry.ip == ip && ArpEntry.mac == mac) )
            {
                Logger.log("ARP duplicate entry, do not insert..");
            }
            else
            {
                // perhaps change, can same ip have more mac and vice versa?
                ArpEntry record = new ArpEntry(ip, mac, time.Ticks, netInterface);
                arpTable.Add(record);
                Logger.log("ARP cache update");
                Controller.Instance.gui.refresArpTable();
            }

        }

        public void arping(int interfaceIndex, string ip)
        {
            Packet p = ArpBuilder.BuildRequest(Controller.Instance.netInterfaces[interfaceIndex], new IpV4Address(ip));
            Controller.Instance.communicators[interfaceIndex].inject(p);
        }

        public override void forwardPacketToProcessor(Packet packet, Comminucator com)
        {
            throw new NotImplementedException();
        }

        public override void process(Packet packet, Comminucator com)
        {
            MacAddress m;
            bool mergeFlag = false;
            ArpEntry entry = searchArpCache(packet.Ethernet.Arp.SenderProtocolIpV4Address);
            if (entry != null)
            {
                Utils.ByteToMac(packet.Ethernet.Arp.SenderHardwareAddress, out m);
                entry.mac = m;
                entry.time = DateTime.Now.Ticks;
                mergeFlag = true;
            }

            // if packet is for me
            if (packet.Ethernet.Arp.TargetProtocolIpV4Address == com._netInterface.IpV4Address || packet.Ethernet.Arp.TargetProtocolIpV4Address.ToValue() == 0)
            {
                if (mergeFlag == false)
                {
                    Utils.ByteToMac(packet.Ethernet.Arp.SenderHardwareAddress, out m);
                    addArp(
                        packet.Ethernet.Arp.SenderProtocolIpV4Address,
                        m,
                        DateTime.Now,
                        com._netInterface.id
                    );
                }
                if (packet.Ethernet.Arp.Operation == PcapDotNet.Packets.Arp.ArpOperation.Request)
                {
                    Packet response = ArpBuilder.BuildRepy(packet, com._netInterface);
                    com.inject(response);
                    
                }
            } else // proxy arp
            {
                Route r = RoutingTable.Instance.search(packet.Ethernet.Arp.TargetProtocolIpV4Address);
                if (r != null)
                {
                    Logger.log("Proxy arp - " + packet.Ethernet.Arp.TargetProtocolIpV4Address);
                    Packet response = ArpBuilder.BuildRepy(packet, com._netInterface);
                    com.inject(response);
                }
            }
            Controller.Instance.gui.refresArpTable();
        }

        public void setCacheTimeout(string timeout)
        {
            int value = 3;
            if (int.TryParse(timeout, out value))
            {
                this.arpCacheTimeout = value;
            }
        }

        private MacAddress ByteToMac(ReadOnlyCollection<byte> byteCollection)
        {
            var str = new System.Text.StringBuilder();
            str.Append(byteCollection[0].ToString("X"));
            for (int i = 1; i < byteCollection.Count; i++)
            {
                str.AppendFormat(":{0}", byteCollection[i].ToString("X"));
            }
            MacAddress mac = new MacAddress(str.ToString());
            return mac;
        }
    }
}

