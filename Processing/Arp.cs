using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcapDotNet.Packets;
using System.Data;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.Ethernet;


namespace sw_router.Processing
{
    class Arp : Processing
    {
        /* SINGLETON START*/
        private static readonly Lazy<Arp> _instance = new Lazy<Arp>(() => new Arp());
        public static Arp Instance
        {
            get
            {
                return _instance.Value;
            }
        }
        /* SINGLETON END*/

        public DataTable arpTable { get; }

        private Arp()
        {
            arpTable = new DataTable();
            arpTable.Columns.Add("ip", typeof(IpV4Address));
            arpTable.Columns.Add("mac", typeof(MacAddress));
            arpTable.Columns.Add("time", typeof(DateTime));
            arpTable.Columns.Add("interface", typeof(int));

            addArp(new IpV4Address(0), new MacAddress("00:00:00:00:00:00"), DateTime.Now, 0);
        }

        public void flushArp()
        {
            arpTable.Rows.Clear();
        }

        public void addArp(IpV4Address ip, MacAddress mac, DateTime time, int netInterface)
        {
            DataRow row = arpTable.NewRow();
            row["ip"] = ip;
            row["mac"] = mac;
            row["time"] = time;
            row["interface"] = netInterface;
            arpTable.Rows.Add(row);
        }

        public override void forwardPacketToProcessor(Packet packet)
        {
            throw new NotImplementedException();
        }

        public override void process(Packet packet)
        {
            
        }
    }
}

