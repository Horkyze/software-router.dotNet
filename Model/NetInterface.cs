using PcapDotNet.Core;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Ethernet;
using PcapDotNet.Packets.IpV4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sw_router
{
    public class NetInterface
    {

        public LivePacketDevice PcapDevice { get; set; }
        public IpV4Address IpV4Address { get; set; }
        public MacAddress MacAddress { get; set; }
        public int NetMask { get; set; }
        public int id { get; set; }

        public NetInterface(LivePacketDevice d, int id)
        {
            Logger.log("Creating NetInterface ["+id+"] "+d.Description);
            PcapDevice = d;
            this.id = id;
        }

    }
}
