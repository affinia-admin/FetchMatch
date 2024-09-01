using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace FetchMatch.Package
{
    public class MatchStoreUtil
    {
        private AmazonDynamoDBClient _client;

        public MatchStoreUtil() 
        {
            _client = new AmazonDynamoDBClient(Amazon.RegionEndpoint.USWest1);
        }

        public async Task<MatchStore> RetrieveMatch(string shortkey)
        {
            MatchStore match = new MatchStore();
            var rack_0_key = DDBUtil.TableKey(DDBUtil.PRIMARY_KEY_COLUMN, shortkey, DDBUtil.SECONDARY_KEY_COLUMN,
                DDBUtil.INT_VALUE_TYPE, DDBUtil.ZERO_STR);
            var initRackRequest = new GetItemRequest
            {
                TableName = DDBUtil.TABLE_LIVEMATCH,
                Key = rack_0_key
            };

            var matchDetailRes = await _client.GetItemAsync(initRackRequest);
            if (matchDetailRes != null && matchDetailRes.Item.ContainsKey(DDBUtil.PLAYER_COLUMN) == true)
            {
                Console.Error.WriteLine("matchkey" + rack_0_key);
                match.PlayerNum = shortkey;
                match.CT = matchDetailRes.Item[DDBUtil.CREATE_COLUMN].S;
                match.City = matchDetailRes.Item[DDBUtil.CITY_COLUMN].S;
                match.MF = matchDetailRes.Item[DDBUtil.MATCH_FORMAT_COLUMN].S;
                match.SM = matchDetailRes.Item[DDBUtil.SCORE_MODE_COLUMN].S;
                match.RN = int.Parse(matchDetailRes.Item[DDBUtil.RACK_NUM_COLUMN].S);
                match.TPI = int.Parse(matchDetailRes.Item[DDBUtil.TURN_PLAYER_COLUMN].S);
                List<AttributeValue> player_attributes = matchDetailRes.Item[DDBUtil.PLAYER_COLUMN].L;
                List<Player> players = new List<Player>();
                int pl_index = 0;
                foreach (var attribute in player_attributes)
                {
                    Player player = new Player();
                    player.PlayerNum = attribute.S;
                    player.Idx = pl_index;
                    player.UN = matchDetailRes.Item[DDBUtil.USRN_COLUMN + pl_index.ToString()].S;
                    player.SafePlay = int.Parse(matchDetailRes.Item[DDBUtil.SAFE_PLAY_COLUMN + pl_index.ToString()].S);
                    player.ErrorPlay = int.Parse(matchDetailRes.Item[DDBUtil.ERR_PLAY_COLUMN + pl_index.ToString()].S);
                    player.Score = int.Parse(matchDetailRes.Item[DDBUtil.SCORE_COLUMN + pl_index.ToString()].S);
                    player.MaxScore = int.Parse(matchDetailRes.Item[DDBUtil.MAX_SCORE_COLUMN + pl_index.ToString()].S);
                    player.Skill = int.Parse(matchDetailRes.Item[DDBUtil.SKILL_COLUMN + pl_index.ToString()].S);
                    player.Turns = int.Parse(matchDetailRes.Item[DDBUtil.TURNS_COLUMN + pl_index.ToString()].S);
                    players.Add(player);
                    pl_index++;
                }
                match.Players = players;

                if (match.RN > 0)
                {
                    List<NBPRack> nBPRacks = new List<NBPRack>();

                    for (int i = 1; i <= match.RN; i++)
                    {
                        var rack_req = new GetItemRequest();
                        var rack_key = DDBUtil.TableKey(DDBUtil.PRIMARY_KEY_COLUMN, shortkey,
                            DDBUtil.SECONDARY_KEY_COLUMN, DDBUtil.INT_VALUE_TYPE, i.ToString());
                        var rackRequest = new GetItemRequest
                        {
                            TableName = "RR_LM",
                            Key = rack_key
                        };
                        var rackDetailRes = await _client.GetItemAsync(rackRequest);
                        if (match.MF.Equals("n") && match.SM.Equals("p"))
                        {
                            NBPRack nBPRack = new NBPRack();
                            nBPRack.RackNumber = i;
                            if (!match.RN.Equals(i.ToString()))
                            {
                                nBPRack.EndRack = true;
                            }
                            for (int ni = 1; ni <= 9; ni++)
                            {
                                PoolBall poolBall = new PoolBall();
                                poolBall.BallNumber = ni;
                                if (rackDetailRes.Item.ContainsKey(DDBUtil.GetBallColumns(ni.ToString())))
                                {
                                    poolBall.BallOwner = int.Parse(rackDetailRes.Item[DDBUtil.GetBallColumns(ni.ToString())].S);
                                }
                                if (!match.RN.Equals(i.ToString()) && !rackDetailRes.Item.ContainsKey(DDBUtil.GetBallColumns(ni.ToString())))
                                {
                                    poolBall.BallOwner = -1;
                                }

                                if (ni == 9)
                                {
                                    poolBall.RackWinner = true;
                                }
                                nBPRack.Balls.Add(poolBall);
                            }
                            nBPRacks.Add(nBPRack);
                        }
                        if (match.MF.Equals("e") && match.SM.Equals("p"))
                        {

                        }
                        if (match.SM.Equals("g"))
                        {

                        }
                        if (match.MF.Equals("s"))
                        {

                        }

                    }
                    match.NBPS = nBPRacks;
                }
                match.MS = "s";
                return match;
            }
            match.MS = "NF";
            return match;
        }
    }
}
