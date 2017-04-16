using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sw_router
{
    public class Stats
    {
        public int pakcets_out { get; set; }
        public int pakcets_in { get; set; }

        public Stats()
        {
            this.pakcets_in = 0;
            this.pakcets_out = 0;
        }
    }

    public class Timers
    {
        public int UPDATE { get; set; } = 10;
        public int INVALID { get; set; } = 40;
        public int HOLD_DOWN { get; set; } = 40; // not used anyway
        public int FLUSH { get; set; } = 70;
    }
}
