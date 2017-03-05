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
        private static MacAddress _broadcastAddress = new MacAddress("FF:FF:FF:FF:FF:FF");

        /// <summary>
        /// This function build an ARP over Ethernet packet.
        /// </summary>
        public static Packet BuildRepy(Packet request, NetInterface netInterface)
        {
            EthernetLayer ethernetLayer =
                new EthernetLayer
                {
                    Source = netInterface.MacAddress,
                   
                    Destination = request.Ethernet.Source,
                    EtherType = EthernetType.None, 
                };

            ArpLayer arpLayer =
                new ArpLayer
                {
                    ProtocolType = EthernetType.IpV4,
                    Operation = ArpOperation.Reply,
                    SenderHardwareAddress =
                        (request.Ethernet.Arp.TargetHardwareAddress.AsReadOnly() == Utils.AddressToByte(null, _broadcastAddress).AsReadOnly()) ?
                        Utils.AddressToByte(null, _broadcastAddress).AsReadOnly() : Utils.AddressToByte(null, netInterface.MacAddress).AsReadOnly(),
                    SenderProtocolAddress = Utils.AddressToByte(netInterface.IpV4Address, null).AsReadOnly(),
                    TargetHardwareAddress = request.Ethernet.Arp.SenderHardwareAddress.AsReadOnly(),
                    TargetProtocolAddress = request.Ethernet.Arp.SenderProtocolAddress.AsReadOnly(),
                };

            PacketBuilder builder = new PacketBuilder(ethernetLayer, arpLayer);

            return builder.Build(DateTime.Now);
        }
    }
}
