using PcapDotNet.Packets;
using PcapDotNet.Packets.Ethernet;
using PcapDotNet.Packets.IpV4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sw_router.Builder
{
    class Builder
    {
        public static Packet createCopy(Packet packet)
        {
            var ethLayer = (EthernetLayer)packet.Ethernet.ExtractLayer();
            var ipLayer = (IpV4Layer)packet.Ethernet.IpV4.ExtractLayer();
            var ipPayload = (PayloadLayer)packet.Ethernet.IpV4.Payload.ExtractLayer();

            ILayer[] layers = { ethLayer, ipLayer, ipPayload };

            var pkt = new PacketBuilder(layers).Build(DateTime.Now);
            
            return pkt;
        }
    }
}
