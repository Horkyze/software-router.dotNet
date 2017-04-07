using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Ethernet;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.Icmp;

namespace sw_router.Processing
{
    class Icmp : Processing
    {
        /* SINGLETON START*/
        private static readonly Lazy<Icmp> _instance = new Lazy<Icmp>(() => new Icmp());
        public static Icmp Instance
        {
            get
            {
                return _instance.Value;
            }
        }
        /* SINGLETON END*/

        public override void forwardPacketToProcessor(Packet packet, Comminucator com)
        {
            throw new NotImplementedException();
        }

        public override void process(Packet packet, Comminucator com)
        {
            // packet is for me
            if (packet.Ethernet.IpV4.Destination == com._netInterface.IpV4Address &&
                packet.Ethernet.Destination == com._netInterface.MacAddress)
            {
                if(packet.Ethernet.IpV4.Icmp.MessageType == IcmpMessageType.Echo)
                {
                    // send reply
                    var ethLayer = (EthernetLayer)packet.Ethernet.ExtractLayer();
                    var ipLayer = (IpV4Layer)packet.Ethernet.IpV4.ExtractLayer();
                    var icmpHdr = (IcmpEchoLayer)packet.Ethernet.IpV4.Icmp.ExtractLayer();
                    var icmpPayload = (PayloadLayer)packet.Ethernet.IpV4.Icmp.Payload.ExtractLayer();

                    ethLayer.Destination = Arp.Instance.get(packet.Ethernet.IpV4.Source, com._netInterface.id);
                    ethLayer.Source = com._netInterface.MacAddress;
                    
                    ipLayer.CurrentDestination = ipLayer.Source;
                    ipLayer.Source = com._netInterface.IpV4Address;

                    var replyLayer = new IcmpEchoReplyLayer();
                    replyLayer.SequenceNumber = icmpHdr.SequenceNumber;
                    replyLayer.Identifier = icmpHdr.Identifier;
                    
                    ILayer[] layers = { ethLayer, ipLayer, replyLayer, icmpPayload };

                    var pkt = new PacketBuilder(layers).Build(DateTime.Now);
                    Controller.Instance.communicators[com._netInterface.id].inject(pkt);
                }
            }
        }
    }
}
