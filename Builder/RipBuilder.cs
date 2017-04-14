using PcapDotNet.Packets;
using PcapDotNet.Packets.Ethernet;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.Transport;
using sw_router.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sw_router.Builder
{
    class RipBuilder
    {
        public static Packet BuildUpdate(NetInterface netInterface, byte[] ripData, int len)
        {
            EthernetLayer ethernetLayer =
                new EthernetLayer
                {
                    Source = netInterface.MacAddress,
                    Destination = RipRouteEntry.MULTICAST_MAC,
                    EtherType = EthernetType.IpV4,
                };

            IpV4Layer ip = new IpV4Layer
            {
                CurrentDestination = new IpV4Address(RipRouteEntry.BROADCAST_IP.ToValue()),
                Source = new IpV4Address(netInterface.IpV4Address.ToValue()),
                Identification = 0,
                Protocol = IpV4Protocol.Udp,
                Ttl = 2,
                HeaderChecksum = null,
                Fragmentation = IpV4Fragmentation.None,
                Options = IpV4Options.None,
                TypeOfService = 0
            };

            UdpLayer udp = new UdpLayer
            {
                SourcePort = RipRouteEntry.UDP_PORT,
                DestinationPort = RipRouteEntry.UDP_PORT,
                CalculateChecksumValue = true,
                Checksum = null
            };

            PayloadLayer rip = new PayloadLayer
            {
                Data = new Datagram(ripData)
                
            };

            PacketBuilder builder = new PacketBuilder(ethernetLayer, ip, udp, rip);
            Packet p = builder.Build(DateTime.Now);
            return p;
        }

        public static Packet BuildRequest(NetInterface netInterface)
        {
            EthernetLayer ethernetLayer =
                new EthernetLayer
                {
                    Source = netInterface.MacAddress,
                    Destination = RipRouteEntry.MULTICAST_MAC,
                    EtherType = EthernetType.IpV4,
                };

            IpV4Layer ip = new IpV4Layer
            {
                CurrentDestination = new IpV4Address(RipRouteEntry.BROADCAST_IP.ToValue()),
                Source = new IpV4Address(netInterface.IpV4Address.ToValue()),
                Identification = 0,
                Protocol = IpV4Protocol.Udp,
                Ttl = 2,
                HeaderChecksum = null,
                Fragmentation = IpV4Fragmentation.None,
                Options = IpV4Options.None,
                TypeOfService = 0
            };

            UdpLayer udp = new UdpLayer
            {
                SourcePort = RipRouteEntry.UDP_PORT,
                DestinationPort = RipRouteEntry.UDP_PORT,
                CalculateChecksumValue = true,
                Checksum = null
            };

            PayloadLayer rip = new PayloadLayer
            {
                Data = new Datagram(Utils.ConvertHexStringToByteArray("010200000000000000000000000000000000000000000010"))
            };

            PacketBuilder builder = new PacketBuilder(ethernetLayer, ip, udp, rip);
            Packet p = builder.Build(DateTime.Now);
            return p;
        }
    }
}
