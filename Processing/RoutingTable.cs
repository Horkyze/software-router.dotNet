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

        public override string ToString()
        {
            return network.ToString() + "/" + mask.ToString() + " ad " + ad.ToString() + " via " + nextHop.ToString() + " interface " +outgoingInterfate.ToString();
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
        private List<Route> potecialRoutes = new List<Route>();

        public RoutingTable()
        {
            table = new List<Route>();      
        }

        public void addRoute(Route route)
        {
            for (int i = 0; i < table.Count; i++)
            {
                Route r = table.ElementAt(i);
                if (r.ToString() == route.ToString())
                {
                    Logger.log("Cannot add same route twice");
                    return;
                }
               
            }

            table.Add(route);
            Controller.Instance.gui.updateRoutingTable();
        }

        public void delete(string route)
        {
            for (int i = 0; i < table.Count; i++)
            {
                Route r = table.ElementAt(i);
                if (r.ToString() == route)
                {
                    if (r.ad == Route.DIRECTLY_CONNECTED_AD)
                    {
                        Logger.log("Cannor remove directly connected route");
                        break;
                    }
                    else
                    {
                        table.RemoveAt(i);
                        break;
                    }
                    
                }
            }
        }

        public Route search(IpV4Address dstIp)
        {
            Route best = null;
            this.potecialRoutes.Clear();
            getAllRoutes(dstIp);
            

            foreach (Route r in this.potecialRoutes)
            {
                if (best == null)
                    best = r;

                if (r.ad < best.ad || r.metric < best.metric || r.mask > best.mask)
                    best = r;
            }
            if (best != null)
            {
                Logger.log("Potencial routes for " + dstIp.ToString());
                foreach (Route r in this.potecialRoutes)
                {
                    if(r.ToString() == best.ToString())
                        Logger.log("> " + r.ToString());
                    Logger.log("  " + r.ToString());

                }
            }
            else
            {
                Logger.log("No route for " + dstIp.ToString());
            }
            
            return best;
        }

        private void getAllRoutes(IpV4Address dstIp)
        {
            for (int i = 0; i < table.Count; i++)
            {
                Route r = table.ElementAt(i);
                if (Utils.belongsToSubnet(dstIp, r.mask, r.network))
                {

                    if (r.outgoingInterfate == 0 || r.outgoingInterfate == 1)
                    {                      
                        this.potecialRoutes.Add(r);
                        return;
                    }
                    if (r.outgoingInterfate == -1)
                    {
                        // perform recurvice routing search
                        Logger.log("Recursive route search with " + r.nextHop.ToString());
                        getAllRoutes(r.nextHop);
                    }
                }
            }
        }

        public void updateDirectlyConnected()
        {
            for (int i = 0; i < table.Count; i++)
            {
                Route r = table.ElementAt(i);
                if(r.ad == Route.DIRECTLY_CONNECTED_AD)
                {
                    table.RemoveAt(i);
                    i--;
                }
            }

            // add static routes
            for (int a = 0; a < 2; a++)
            {
                NetInterface i = Controller.Instance.netInterfaces[a];
                this.addRoute(new Route(i.IpV4Address, i.NetMask, Route.DIRECTLY_CONNECTED_AD, 0, new IpV4Address(), a));
            }
        }
    }
}
