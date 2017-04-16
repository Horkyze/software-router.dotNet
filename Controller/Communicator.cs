using PcapDotNet.Core;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Ethernet;
using PcapDotNet.Packets.IpV4;
using sw_router.Processing;
using System;
using System.Threading;

namespace sw_router
{
    public class Comminucator
    {

        public Stats stats;
        public PacketCommunicator com { get; set; }
        public NetInterface _netInterface;
        private Thread _listenThread = null;


        public Comminucator(NetInterface i)
        {
            this.stats = new Stats();
            Logger.log("Creating Comminucator (netInferdate.id = "+i.id);

            _netInterface = i;
            com = _netInterface.PcapDevice.Open(
                65536, 
                PacketDeviceOpenAttributes.NoCaptureLocal | PacketDeviceOpenAttributes.Promiscuous, 
                1000
            );

        }

        public void SetUpThread(String id)
        {
            _listenThread = new Thread(new ThreadStart(listen))
            {
                Name = id,
                IsBackground = true
            };
            _listenThread.Start();
        }


        // high level processing
        private void ProcessCapturedPacket(Packet packet)
        {
            
            if (packet.Ethernet.Source == _netInterface.MacAddress)
            {
                Logger.log("Source MAC is my ...Dropping packet");
                return;                
            }

            if (packet.DataLink.Kind != DataLinkKind.Ethernet)
            {
                Logger.log("DataLink is not Ehterhet ... Drop");
                return;
            }

            stats.pakcets_in++;
            if (packet.Ethernet.EtherType == EthernetType.Arp)
            {
                Logger.log("Got arp");
                Arp.Instance.process(packet, this);
                return;
            }
            
            // ICMP
            if (packet.Ethernet.IpV4.Protocol == IpV4Protocol.InternetControlMessageProtocol)
            {
                if (packet.Ethernet.IpV4.Destination == _netInterface.IpV4Address &&
                    packet.Ethernet.Destination == _netInterface.MacAddress)
                    Icmp.Instance.process(packet, this);
                //return;
            }
                        
            //dont process non-IPv4 packets..
            if (packet.Ethernet.EtherType != EthernetType.IpV4)
                return;

            if (packet.Ethernet.Destination == RipRouteEntry.MULTICAST_MAC &&
                packet.Ethernet.IpV4.Protocol == IpV4Protocol.Udp &&
                packet.Ethernet.IpV4.Destination == RipRouteEntry.BROADCAST_IP &&
                packet.Ethernet.IpV4.Udp.DestinationPort == RipRouteEntry.UDP_PORT &&
                packet.Ethernet.IpV4.Udp.SourcePort == RipRouteEntry.UDP_PORT
                
                )
            { 
                Rip.Instance.process(packet, this);
            }

            // dont process broadcast ips
            if (Utils.isIPv4Broadcast(packet.Ethernet.IpV4.Destination, this._netInterface.NetMask, this._netInterface.IpV4Address))
            {
                //Logger.log("Drop broadcast..");
                return;
            }
            // dont process multicast ips
            if (Utils.isIPv4Multicast(packet.Ethernet.IpV4.Destination))
            {
                //Logger.log("Drop multicast..");
                return;
            }

            // IP packet just for me?
            if (packet.Ethernet.IpV4.Destination == this._netInterface.IpV4Address)
            {
                // nah, i only ping out, drop..., or? TODO
                return;
            }
            
            // find route for packet
            Route r = RoutingTable.Instance.search(packet.Ethernet.IpV4.Destination);
            if (r != null)
            {

                // do not reply to the same interface?? TODO !!!
                if (r.outgoingInterfate == this._netInterface.id)
                {
                    return;
                }
                var ethLayer = (EthernetLayer)packet.Ethernet.ExtractLayer();
                var ipLayer = (IpV4Layer)packet.Ethernet.IpV4.ExtractLayer();
                var ipPayload = (PayloadLayer)packet.Ethernet.IpV4.Payload.ExtractLayer();

                MacAddress got = Arp.Instance.get(packet.Ethernet.IpV4.Destination, r.outgoingInterfate);
                if ( got == new MacAddress("aa:ff:ff:ff:ff:aa") )
                {
                    return;
                }
                ethLayer.Destination = got;
                ethLayer.Source = Controller.Instance.netInterfaces[r.outgoingInterfate].MacAddress;
                if(ipLayer.Ttl < 2)
                {
                    Logger.log("TTL < 2 -> drop packet");
                    return;
                }
                ipLayer.Ttl -= (byte)1;
                ipLayer.HeaderChecksum = null;

                ILayer[] layers = { ethLayer, ipLayer, ipPayload };

                var pkt = new PacketBuilder(layers).Build(DateTime.Now);
                Controller.Instance.communicators[r.outgoingInterfate].inject(pkt);
            }
            
        }

        public void inject(Packet packet)
        {
            stats.pakcets_out++;
            com.SendPacket(packet);
            Logger.log("Inject int " + _netInterface.id );
        }

        public void listen()
        {
            lock (Controller.Instance.locker)
            {
                Logger.log("Hello from linsten thread: " + _netInterface.id);
                Logger.log("  IP:    " + _netInterface.IpV4Address);
                Logger.log("  Mask:  " + _netInterface.NetMask);
                Logger.log("  MAC:   " + _netInterface.MacAddress);            
            }
            com.ReceivePackets(-1, ProcessCapturedPacket);

        }
    }
}