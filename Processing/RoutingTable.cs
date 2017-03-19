using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcapDotNet.Packets.IpV4;
using System.ComponentModel;

namespace sw_router.Processing
{
    class Route
    {
        public static int DIRECTLY_CONNECTED_AD = 0;
        public static int STATIC_AD = 1;
        public static int RIP_AD = 110;


        public IpV4Address network { get; set; }
        public int mask { get; set; }
        public int ad { get; set; }
        public int metric { get; set; }
        public IpV4Address nextHop { get; set; }
        public int outgoingInterfate { get; set; }

        public Route(IpV4Address n, int m, int a, int me, IpV4Address next, int i )
        {
            this.network = n;
            this.mask = m;
            this.ad = a;
            this.metric = me;
            this.nextHop = next;
            this.outgoingInterfate = i;
        }
    }

    class RoutingTable
    {
        /* SINGLETON START*/
        private static readonly Lazy<RoutingTable> _instance = new Lazy<RoutingTable>(() => new RoutingTable());
        public static RoutingTable Instance
        {
            get
            {
                return _instance.Value;
            }
        }
        /* SINGLETON END*/
        private Object _tableLock = new Object();
        public List<Route> table;

        public RoutingTable()
        {
            table = new List<Route>();      
        }

        public void addRoute(Route r)
        {
            table.Add(r);
            Controller.Instance.gui.updateRoutingTable();
        }

        public void updateDirectlyConnected()
        {
            // add static routes
            for (int a = 0; a < 2; a++)
            {
                NetInterface i = Controller.Instance.netInterfaces[a];
                this.addRoute(new Route(i.IpV4Address, i.NetMask, Route.DIRECTLY_CONNECTED_AD, 0, new IpV4Address(), a));
            }
        }
    }
}
