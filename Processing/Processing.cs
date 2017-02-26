using PcapDotNet.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sw_router.Processing
{
    public abstract class Processing
    {
        abstract public void process(Packet packet);
        abstract public void forwardPacketToProcessor(Packet packet);

    }

}

