using PcapDotNet.Packets;
using PcapDotNet.Packets.Ethernet;
using PcapDotNet.Packets.Icmp;
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
    class IcmpBuilder
    {
        public static Packet BuildIcmpRequest(NetInterface netInterface, IpV4Address dest, MacAddress target)
        {
            EthernetLayer ethernetLayer =
                new EthernetLayer
                {
                    Source = netInterface.MacAddress,
                    Destination = target,
                    EtherType = EthernetType.IpV4,
                };

            IpV4Layer ip = new IpV4Layer
            {
                CurrentDestination = dest,
                Source = new IpV4Address(netInterface.IpV4Address.ToValue()),
                Identification = 0,
                Protocol = IpV4Protocol.InternetControlMessageProtocol,
                Ttl = 128,
                HeaderChecksum = null,
                Fragmentation = IpV4Fragmentation.None,
                Options = IpV4Options.None
            };
            IcmpEchoLayer icmp = new IcmpEchoLayer()
            {
                Checksum = null,
                Identifier = 666,
                SequenceNumber = 666
            };
            PayloadLayer payload = new PayloadLayer
            {
                Data = new Datagram(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, }.Concat(Utils.getWANTbytes()).ToArray() )
            };

            PacketBuilder builder = new PacketBuilder(ethernetLayer, ip, icmp, payload);
            Packet p = builder.Build(DateTime.Now);
            return p;
        }
    }
}
