using PcapDotNet.Base;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Arp;
using PcapDotNet.Packets.Ethernet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sw_router.Builder
{
    public class ArpBuilder
    {
        /// <summary>
        /// This function build an ARP over Ethernet packet.
        /// </summary>
        public static Packet BuildRequest(Packet request)
        {
            EthernetLayer ethernetLayer =
                new EthernetLayer
                {
                    Source = request.Ethernet.Destination,
                    Destination = request.Ethernet.Source,
                    EtherType = EthernetType.None, // Will be filled automatically.
                };

            ArpLayer arpLayer =
                new ArpLayer
                {
                    ProtocolType = EthernetType.IpV4,
                    Operation = ArpOperation.Request,
                    SenderHardwareAddress = new byte[] { 3, 3, 3, 3, 3, 3 }.AsReadOnly(), // 03:03:03:03:03:03.
                    SenderProtocolAddress = new byte[] { 1, 2, 3, 4 }.AsReadOnly(), // 1.2.3.4.
                    TargetHardwareAddress = new byte[] { 4, 4, 4, 4, 4, 4 }.AsReadOnly(), // 04:04:04:04:04:04.
                    TargetProtocolAddress = new byte[] { 11, 22, 33, 44 }.AsReadOnly(), // 11.22.33.44.
                };

            PacketBuilder builder = new PacketBuilder(ethernetLayer, arpLayer);

            return builder.Build(DateTime.Now);
        }
    }
}
