using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FetchMatch.Package
{
    public class MatchStore
    {
        private string _userKey = string.Empty;
        private string _username = string.Empty;

        [DynamoDBRangeKey("UK")]
        public string PlayerNum { get; set; } = string.Empty;

        [DynamoDBProperty("UN")]
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 5 || value.Length > 15) { _username = string.Empty; }
                _username = value;
            }
        }
        public string CT { get; set; } = string.Empty;
        public List<Player> Players { get; set; } = new List<Player>();

        public string MF { get; set; } = string.Empty;

        public string MS { get; set; } = string.Empty;

        public string SM { get; set; } = string.Empty;
        public int RN { get; set; } = 0;

        public int TPI { get; set; } = 0;
        public int WI { get; set; } = -1;

        public int SRN { get; set; } = 1;
        public int DB { get; set; } = 0;
        public string City { get; set; } = string.Empty;

        public List<EBPRack> EBPS { get; set; } = new List<EBPRack> { };

        public List<NBPRack> NBPS { get; set; } = new List<NBPRack> { };

        public List<GameRack> GRK { get; set; } = new List<GameRack> { };

        
    }
    }
