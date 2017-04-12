using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcapDotNet.Packets;
using PcapDotNet.Packets.IpV4;
using System.Collections.ObjectModel;
using PcapDotNet.Packets.Ethernet;
using System.Net;
using sw_router.Builder;
using System.Threading;

namespace sw_router.Processing
{
    class RipHeader
    {
        static public byte RIP_REQUEST = 0x01;
        static public byte RIP_RESPONSE = 0x02;
        static public byte RIP_VERSION = 0x02;
        static public UInt32 RIP_INFINITY = 16;

        public byte command { set; get; }
        public byte version { set; get; }
        public ushort zeros { set; get; }
    }
    class RipRouteEntry
    {     
        static public IpV4Address BROADCAST_IP = new IpV4Address("224.0.0.9");
        static public MacAddress MULTICAST_MAC = new MacAddress("01:00:5E:00:00:09");
        static public UInt16 UDP_PORT = 520;

        public ushort family { set; get; }
        public ushort route_tag { set; get; }
        public IpV4Address ip { set; get; }
        public IpV4Address mask { set; get; }
        public IpV4Address next_hop { set; get; }
        public UInt32 metric { set; get; } // hop count

        // only for rip db 
        public enum State { ACTIVE=0, INVALID=1, HOLD_DOWN=2, FLUSH=3 }
        public long insert_time { set; get; }
        public long update_time { set; get; }
        public State state;
        public int recieveInterface = -1;
        public bool insertedManually = false;

        public string ToString()
        {
            return this.ip + " " + this.mask + " " + this.next_hop + " " + this.metric;
        }
    }
    class RipData
    {
        public RipHeader hdr = new RipHeader();
        public List<RipRouteEntry> entries = new List<RipRouteEntry>();

        // parse and create
        public RipData(string hex)
        {
            // 1 bytes
            hdr.command = Convert.ToByte( hex.Substring(0, 2) );

            // 1 bytes
            hdr.version = Convert.ToByte( hex.Substring(2, 2) );

            // 2 bytes
            hdr.zeros = (ushort)IPAddress.NetworkToHostOrder( Convert.ToByte( hex.Substring(4, 4) ) );

            int n_of_entries = hex.Length / 40;
            int main_offset = 8;
            for (int i = 1; i <= n_of_entries; i++)
            {
                int offset = (i - 1) * 40 + main_offset;
                RipRouteEntry entry = new RipRouteEntry();
                entry.family    = (UInt16)Convert.ToUInt16(hex.Substring(offset, 4), 16);
                entry.route_tag = (UInt16)Convert.ToUInt16(hex.Substring(offset + 4, 4), 16);
                entry.ip        = new IpV4Address(Convert.ToUInt32(hex.Substring(offset + 8, 8), 16));
                entry.mask      = new IpV4Address(Convert.ToUInt32(hex.Substring(offset + 16, 8), 16));
                entry.next_hop  = new IpV4Address(Convert.ToUInt32(hex.Substring(offset + 24, 8), 16));
                entry.metric    = Convert.ToUInt32(hex.Substring(offset + 32, 8), 16);
                entries.Add(entry);
            }
        }
    }

    class Rip : Processing
    {
        /* SINGLETON START*/
        private static readonly Lazy<Rip> _instance = new Lazy<Rip>(() => new Rip());
        public static Rip Instance
        {
            get
            {
                return _instance.Value;
            }
        }
        /* SINGLETON END*/

        class Timers
        {
            public static int UPDATE = 10;
            public static int INVALID = 40;
            public static int HOLD_DOWN = 40;
            public static int FLUSH = 80;
        }

        public Rip()
        {
            rip_timers_t = new Thread(new ThreadStart(ripTimers));
            rip_timers_t.Start();
        }

        public Thread rip_timers_t;
        public bool RipEnabled = false;
        public List<RipRouteEntry> RipDatabase = new List<RipRouteEntry>();
        public List<IpV4Address> networks = new List<IpV4Address>();
        
        public void updateRipDb(RipRouteEntry entry)
        {
            foreach (var item in RipDatabase)
            {
                if(item.ip == entry.ip && item.mask == item.mask)
                {
                    item.update_time = Utils.epoch();
                    if(item.metric <= entry.metric)
                    {

                    } else
                    {

                    }
                    return;
                }
            }

            // if we get here, entry was not found in db
            entry.insert_time = Utils.epoch();
            entry.update_time = Utils.epoch();
            entry.state = RipRouteEntry.State.ACTIVE;
            RipDatabase.Add(entry);
            Controller.Instance.gui.updateRipDb();
        }

        public Route getRoute(RipRouteEntry entry)
        {
            Route r;
            int count = RoutingTable.Instance.table.Count;
            for (int i = 0; i < count; i++)
            {
                r = RoutingTable.Instance.table.ElementAt(i);
                if (r.ad == Route.RIP_AD && r.network == entry.ip)
                {
                    return r;
                }
            }
            return null;
        }

        public void updateRoutingTable()
        {
            foreach (var item in RipDatabase)
            {
                Route r = getRoute(item);
                if (r == null && item.state == RipRouteEntry.State.ACTIVE)
                {
                    RoutingTable.Instance.addRoute(new Route(item.ip, Utils.getPrefix(item.mask), Route.RIP_AD, (int)item.metric, item.next_hop, -1));
                }
            }
        }

        public override void forwardPacketToProcessor(Packet packet, Comminucator com)
        {
            throw new NotImplementedException();
        }

        public override void process(Packet packet, Comminucator com)
        {
            if (!RipEnabled)
            {
                Logger.log("Recieved RIP packet, but RIP is disabled...");
                return;
            }

            var rip_raw_data = packet.Ethernet.IpV4.Udp.Payload.ToHexadecimalString();

            RipData ripData = new RipData(rip_raw_data);
            foreach (var entry in ripData.entries)
            {
                // here parsing route entries from update packet
                if (entry.next_hop == new IpV4Address("0.0.0.0"))
                    entry.next_hop = packet.Ethernet.IpV4.Source;

                entry.recieveInterface = com._netInterface.id;
                updateRipDb(entry);
            }
            updateRoutingTable();
        }

        public void sendUpdates()
        {
            int len;
            Packet p;
            byte[] bytes;
            foreach (Route r in RoutingTable.Instance.table)
            {
                if (r.advertiseInRip && r.ad == Route.DIRECTLY_CONNECTED_AD)
                {
                    NetInterface netInt = Controller.Instance.netInterfaces[r.outgoingInterfate];
                    bytes = ToBytes(netInt, out len);
                    var segment = bytes.Subsegment(0, len);
                    p = RipBuilder.BuildUpdate(netInt, segment.ToArray(), len);
                    Logger.log("Sending RIP update from " + netInt.IpV4Address);
                    Controller.Instance.communicators[netInt.id].inject(p);

                }
            }

        }

        public byte[] ToBytes(NetInterface netInterface, out int len)
        {
            byte[] bytes = new byte[4 + 20 * 5];
            bytes.Write(0, 0x02);
            bytes.Write(1, 0x02);
            bytes.Write(2, 0x0000);
            int i = 0, offset;
            len = 4;
            foreach (RipRouteEntry entry in Rip.Instance.RipDatabase)
            {
                if (entry.recieveInterface == netInterface.id)
                    continue;
                IpV4Address new_nexthop = new IpV4Address("0.0.0.0");
                if(entry.next_hop != new_nexthop)
                {
                    new_nexthop = netInterface.IpV4Address;
                }

                offset = 4 + i * 20;
                bytes.Write(offset, entry.family, Endianity.Big);
                bytes.Write(offset + 2, entry.route_tag, Endianity.Big);
                bytes.Write(offset + 4, entry.ip, Endianity.Big);
                bytes.Write(offset + 8, entry.mask, Endianity.Big);
                bytes.Write(offset + 12, new_nexthop, Endianity.Big);
                bytes.Write(offset + 16, entry.metric, Endianity.Big);
                len += 20;
                i++;
            }
            return bytes;
        }

        public void ripTimers()
        {
            do
            {
                if (Rip.Instance.RipEnabled)
                {
                    Logger.log("RIP timers");
                    sendUpdates();
                    Thread.Sleep(1000 * Rip.Timers.UPDATE);
                }
            } while (true);      
        }
    }
}
