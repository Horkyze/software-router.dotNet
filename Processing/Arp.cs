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
        public int time { get; set; }
        public int netInterface { get; set; }


        public ArpEntry(IpV4Address ip, MacAddress mac, int time, int netInterface)
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
        public int arpCacheTimeout = 5*60;

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

            if (entry == null)
            {
                Logger.log("Getting mac (via ARPING) for ip: " + ip.ToString());
                arping(netInterface, ip.ToString());
            }
            else
            {
                Controller.Instance.gui.refresArpTable();
                return entry.mac;
            }

            return new MacAddress("aa:ff:ff:ff:ff:aa");
        }
        

        public void checkOldArp()
        {  
            while (true)
            {
                // Logger.log("Check arp for old");
                int count = arpTable.RemoveAll(e => Utils.epoch() - e.time > arpCacheTimeout);
                if (count > 0)
                {
                    Controller.Instance.gui.refresArpTable();
                    Logger.log("Removed ARP entry");
                }
                
                Thread.Sleep(1000);
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

        public void addArp(IpV4Address ip, MacAddress mac, int time, int netInterface)
        {
            if ( arpTable.Exists(ArpEntry => ArpEntry.ip == ip && ArpEntry.mac == mac) )
            {
                Logger.log("ARP duplicate entry, do not insert..");
            }
            else
            {
                // perhaps change, can same ip have more mac and vice versa?
                ArpEntry record = new ArpEntry(ip, mac, time, netInterface);
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
                entry.time = Utils.epoch();
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
                        Utils.epoch(),
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

