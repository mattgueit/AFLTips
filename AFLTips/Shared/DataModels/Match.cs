using System;
using Newtonsoft.Json;

namespace AFLTips.Shared.DataModels
{
    public class Match
    {
        [JsonProperty("id")]
        public int MatchId { get; set; }

        [JsonProperty("round")]
        public int RoundId { get; set; }

        [JsonProperty("hteamid")]
        public int HomeTeamId { get; set; }

        [JsonProperty("ateamid")]
        public int AwayTeamId { get; set; }

        [JsonProperty("date")]
        public DateTime MatchDate { get; set; }

        [JsonProperty("venue")]
        public string Venue { get; set; }

        [JsonIgnore]
        public DateTime DateUpdated { get; set; }
    }
}
