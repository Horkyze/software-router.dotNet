﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcapDotNet.Packets;
using System.Data;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.Ethernet;
using System.Collections.ObjectModel;
using sw_router.Builder;
using System.Threading;

namespace sw_router.Processing
{
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

        public DataTable arpTable { get; }
        public Thread checkOldArp_t;
        public Object arpCache_lock;

        private Arp()
        {
            arpCache_lock = new Object();

            arpTable = new DataTable();
            arpTable.Columns.Add("ip", typeof(string));
            arpTable.Columns.Add("mac", typeof(string));
            arpTable.Columns.Add("time", typeof(long));
            arpTable.Columns.Add("interface", typeof(int));
            checkOldArp_t = new Thread(new ThreadStart(checkOldArp));
            checkOldArp_t.Start();
            //addArp(new IpV4Address(0)., new MacAddress("00:00:00:00:00:00"), DateTime.Now, 0);
        }

        
        

        public void checkOldArp()
        {
            long value = 0;     
            while (true)
            {
                // Logger.log("Check arp for old");
                for (int i = arpTable.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = arpTable.Rows[i];
                    if ( long.TryParse(dr["time"].ToString(), out value) ) 
                    {                  
                        if (value + 30000000 < DateTime.Now.Ticks)
                            dr.Delete();
                    }  
                }
                try
                {
                    Controller.Instance.gui.refresArpTable();
                }
                catch (Exception e)
                {
                }
                Thread.Sleep(1000);
            }
        }
       
        public void flushArp()
        {
            arpTable.Rows.Clear();
            Controller.Instance.gui.refresArpTable();
        }

        public DataRow searchArpCache(IpV4Address ip)
        {
            DataRow[] result = arpTable.Select("ip = '" + ip.ToString().Trim() + "'" );
            if (result.Length == 1)
            {
                Logger.log("Found matching entry in ARP cache for ip: " + ip);
                return result[0];
            } else if (result.Length > 1)
            {
                Logger.log("?? More entried for in ARP cache for ip: " + ip);
            }
            return null;
        }

        public void addArp(IpV4Address ip, MacAddress mac, DateTime time, int netInterface)
        {
            DataRow row = arpTable.NewRow();
            row["ip"] = ip.ToString().Trim();
            row["mac"] = mac.ToString().ToLower();
            row["time"] = time.Ticks;
            row["interface"] = netInterface;
            arpTable.Rows.Add(row);
            Logger.log("ARP cache update");
            Controller.Instance.gui.refresArpTable();
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
            DataRow row;
            row = searchArpCache(packet.Ethernet.Arp.SenderProtocolIpV4Address);
            if (row != null)
            {
                Utils.ByteToMac(packet.Ethernet.Arp.SenderHardwareAddress, out m);
                row["mac"] = m.ToString().ToLower();
                row["time"] = DateTime.Now.Ticks;
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

            }
            Controller.Instance.gui.refresArpTable();
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

