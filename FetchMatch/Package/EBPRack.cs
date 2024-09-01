using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FetchMatch.Package
{
    public class EBPRack 
    {
        public int RackNumber { get; set; } = 1;

        public int SolidPlayerIndex { get; set; } = 1;

        public int StripePlayerIndex { get; set; } = 1;

        public List<PoolBall> Solids { get; set; } = new List<PoolBall>();
        public List<PoolBall> Stripes { get; set; } = new List<PoolBall>();

        public bool IsPlayersType { get; set; } = false;

        public bool ShowEndRack { get; set; } = false;

        public bool EndRack { get; set; } = false;

        public string Mf { get; set; } = "e";
    }
}
