using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FetchMatch.Package
{
    public class NBPRack
    {
        public int RackNumber { get; set; } = 1;
        public List<PoolBall> Balls { get; set; } = new List<PoolBall>();

        public bool ShowEndRack { get; set; } = false;

        public bool EndRack { get; set; } = false;

        public string Mf { get; set; } = "n";
    }
}
