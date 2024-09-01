using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FetchMatch.Package
{
    public class Player
    {
        private string _userKey = string.Empty;
        private string _username = string.Empty;

        [DynamoDBRangeKey("UK")]
        public string PlayerNum { get; set; }

        [DynamoDBProperty("UN")]
        public string UN
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
        public int Idx { get; set; } = 0;

        public int Skill { get; set; } = 0;

        [DynamoDBProperty("Score")]
        public int Score { get; set; } = 0;

        [DynamoDBProperty("MaxScore")]
        public int MaxScore { get; set; } = 0;

        public int ErrorPlay { get; set; } = 0;

        public int SafePlay { get; set; } = 0;

        public int Turns { get; set; } = 0;

        public List<string> BrkList { get; set; } = new List<string>();
    }
}
