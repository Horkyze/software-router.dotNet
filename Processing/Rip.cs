﻿using System;
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
        public bool markerForRemoval = false;
        public int garbageTimer = 0;

        public override string ToString()
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

        public Timers Timers;

        public Rip()
        {
            Timers = new Timers();
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
                    item.metric = entry.metric;
                    if(entry.metric == RipHeader.RIP_INFINITY && item.markerForRemoval == false)
                    {
                        // delete the route after 3 updates
                        item.markerForRemoval = true;
                        item.garbageTimer = 3;
                    }
                    Controller.Instance.gui.updateRipDb();
                    return;
                }
            }

            if(entry.metric == RipHeader.RIP_INFINITY)
            {
                // no point adding 16 metric to database..
                Logger.log("Recieved route with 16 metric..");
                return;
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
            RoutingTable.Instance.table.RemoveAll(r => r.ad == Route.RIP_AD);
            try
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
            catch (Exception ee)
            {
                Logger.log("collection was modified RipDatabase");
            }
            Controller.Instance.gui.updateRoutingTable();
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
            if (ripData.hdr.version != RipHeader.RIP_VERSION)
            {
                Logger.log("got RIP v1 -> drop..");
                return;
            }
            if (ripData.hdr.command == RipHeader.RIP_RESPONSE)
            {
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
            else if (ripData.hdr.command == RipHeader.RIP_REQUEST)
            {
                sendUpdates();
            }
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
            uint new_hops = 0;
            len = 4;
            try
            {
                foreach (RipRouteEntry entry in Rip.Instance.RipDatabase)
                {
                    if(entry.garbageTimer > 0)
                    {
                        entry.garbageTimer--;
                    }

                    if (entry.recieveInterface == netInterface.id)
                        continue;

                    new_hops = entry.metric;
                    if (entry.recieveInterface != netInterface.id && entry.insertedManually == false)
                    {
                        if (new_hops < 15)
                            new_hops++;
                    }

                    IpV4Address new_nexthop = new IpV4Address("0.0.0.0");
                    if (entry.next_hop != new_nexthop)
                    {
                        new_nexthop = netInterface.IpV4Address;
                    }

                    offset = 4 + i * 20;
                    bytes.Write(offset, entry.family, Endianity.Big);
                    bytes.Write(offset + 2, entry.route_tag, Endianity.Big);
                    bytes.Write(offset + 4, entry.ip, Endianity.Big);
                    bytes.Write(offset + 8, entry.mask, Endianity.Big);
                    bytes.Write(offset + 12, new_nexthop, Endianity.Big);
                    bytes.Write(offset + 16, new_hops, Endianity.Big);
                    len += 20;
                    i++;
                }
            }
            catch (Exception ee)
            {
                Logger.log("Collection may have beed modified Rip.Instance.RipDatabase");
            }
            return bytes;
        }

        public void afterRipDisabled()
        {
            // empty RIP DB
            Rip.Instance.RipDatabase.Clear();

            // delete all RIP routes
            RoutingTable.Instance.table.RemoveAll(table => table.ad == Route.RIP_AD);

            // no longer advertise routes
            RoutingTable.Instance.table.ForEach(table => table.advertiseInRip = false);

            Controller.Instance.gui.updateRipDb();
            Controller.Instance.gui.updateRoutingTable();
            Logger.log("RIP - flushing all rip routes");
        }

        public void ripTimers()
        {
            uint last_update = (uint)Utils.epoch() * 2;
            uint now;
            do
            {
                if (Rip.Instance.RipEnabled)
                {
                    now = (uint)Utils.epoch();

                    // update interval
                    if (now - last_update >= Rip.Instance.Timers.UPDATE)
                    {
                        Logger.log("RIP UPDATE");
                        sendUpdates();
                        last_update = now;
                    }


                    foreach (var entry in Rip.Instance.RipDatabase)
                    {
                        if(entry.insertedManually == true)
                        {
                            continue;
                        }
                        if(now - entry.update_time >= Rip.Instance.Timers.INVALID)
                        {
                            // init invalid state for entry
                            Logger.log("RIP route has reached INVALID state: " + entry);
                            entry.metric = RipHeader.RIP_INFINITY;
                            entry.state = RipRouteEntry.State.INVALID;
                        }

                        if (now - entry.update_time >= Rip.Instance.Timers.FLUSH)
                        {
                            Logger.log("RIP FLUSH TIMER for: " + entry);
                            // delete from routing table and RIP DB 
                            entry.markerForRemoval = true;
                            entry.garbageTimer = 0;
                        }
                    }

                    if( Rip.Instance.RipDatabase.RemoveAll(e => e.markerForRemoval == true && e.garbageTimer == 0) > 0)
                    {
                        Rip.Instance.updateRoutingTable();
                        Controller.Instance.gui.updateRipDb();
                        Controller.Instance.gui.updateRoutingTable();
                    }
                }
                Thread.Sleep(1000);
            } while (true);      
        }
    }
}
