 

namespace FetchMatch.Package
{
    public interface GameRack
    {
        public int RackNumber { get; set; }
        public int RackWinStatus { get; set; }
        public int RackStatus { get; set; }
        public int BreakStatus { get; set; }    
    }
}
