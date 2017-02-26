using PcapDotNet.Core;
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
        public ListenController ListenController { get; }
        public IpV4Address IpV4Address { get; set; }
        public MacAddress MacAddress { get; set; }
        public int NetMask { get; set; }

        public NetInterface(LivePacketDevice d)
        {
            PcapDevice = d;   
        }

        public override string ToString()
        {
            return "asdsa"; //  PcapDevice.Description;
        }

    }
}
