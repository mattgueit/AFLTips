using Newtonsoft.Json;

namespace AFLTips.Shared.DataModels
{
    public class MatchResult
    {
        [JsonProperty("id")]
        public int MatchId { get; set; }

        [JsonProperty("winnerteamid")]
        public int WinnerTeamId { get; set; }

        [JsonProperty("hgoals")]
        public int HomeGoals { get; set; }

        [JsonProperty("hbehinds")]
        public int HomeBehinds { get; set; }

        [JsonProperty("hscore")]
        public int HomeScore { get; set; }

        [JsonProperty("agoals")]
        public int AwayGoals { get; set; }

        [JsonProperty("abehinds")]
        public int AwayBehinds { get; set; }

        [JsonProperty("ascore")]
        public int AwayScore { get; set; }
    }
}
