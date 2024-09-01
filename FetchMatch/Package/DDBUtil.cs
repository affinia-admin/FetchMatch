using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FetchMatch.Package
{
    public class DDBUtil
    {
        public const string TABLE_LIVEMATCH = "RR_LM";
        public const string PRIMARY_KEY_COLUMN = "PK";
        public const string SECONDARY_KEY_COLUMN = "SK";
        public const string STRING_VALUE_TYPE = "s";
        public const string INT_VALUE_TYPE = "i";
        public const string LIST_VALUE_TYPE = "l";
        public const string ZERO_STR = "0";

        public static readonly string MATCH_FORMAT_COLUMN = "MF";
        public static readonly string SCORE_MODE_COLUMN = "SM";
        public static readonly string CITY_COLUMN = "City";
        public static readonly string CREATE_COLUMN = "CT";
        public static readonly string RACK_NUM_COLUMN = "RN";

        public static readonly string SC_COLUMN = "SC";
        public static readonly string PL_COLUMN = "PL";
        public static readonly string DB_COLUMN = "DB";
        public static readonly string TURN_PLAYER_COLUMN = "TP";
        public static readonly string PLAYER_COLUMN = "PL";

        public static readonly string USRN_COLUMN = "Usrn_";
        public static readonly string SKILL_COLUMN = "Sk_";
        public static readonly string SCORE_COLUMN = "Sc_";
        public static readonly string MAX_SCORE_COLUMN = "Mxsc_";
        public static readonly string TURNS_COLUMN = "Tn_";
        public static readonly string SAFE_PLAY_COLUMN = "Sp_";
        public static readonly string ERR_PLAY_COLUMN = "Ep_";

        public static readonly Dictionary<string, string> BALL_COULMNS = new Dictionary<string, string>
        {
            {"1", "ONE" },
            {"2", "TWO" },
            {"3", "THREE" },
            {"4", "FOUR" },
            {"5", "FIVE" },
            {"6", "SIX" },
            {"7", "SEVEN" },
            {"8", "EIGHT" },
            {"9", "NINE" },
            {"10", "TEN" },
            {"11", "ELE" },
            {"12", "TWEL" },
            {"13", "THIR" },
            {"14", "FOURT" },
            {"15", "FIFT" },
        };

        public static Dictionary<string, AttributeValue> TableKey(string PK_COLUMN, string PK, string SK_COLUMN = "",
                 string SK_TYPE = "", string SK = "")
        {
            Dictionary<string, AttributeValue> key = new Dictionary<string, AttributeValue>
            {
                    { PK_COLUMN, new AttributeValue { S = PK } }
            };
            if (!string.IsNullOrEmpty(SK_COLUMN) && !string.IsNullOrEmpty(SK))
            {
                if (!string.IsNullOrEmpty(SK_TYPE) && string.Equals(SK_TYPE, STRING_VALUE_TYPE))
                {
                    key.Add(SK_COLUMN, new AttributeValue { S = SK });
                }
                else if (!string.IsNullOrEmpty(SK_TYPE) && string.Equals(SK_TYPE, INT_VALUE_TYPE))
                {
                    key.Add(SK_COLUMN, new AttributeValue { N = SK });
                }
            }
            return key;
        }

        public static string GetBallColumns(string ballNum)
        {
            BALL_COULMNS.TryGetValue(ballNum, out var result);
            return result;
        }

    }
}
