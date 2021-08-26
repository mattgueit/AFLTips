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
        public int? HomeTeamId { get; set; }

        [JsonProperty("ateamid")]
        public int? AwayTeamId { get; set; }

        [JsonProperty("date")]
        public DateTime MatchDate { get; set; }

        [JsonProperty("venue")]
        public string Venue { get; set; }

        [JsonProperty("complete")]
        public bool Completed { get; set; }

        [JsonProperty("winnerteamid")]
        public int? WinnerTeamId { get; set; }

        [JsonProperty("hgoals")]
        public int? HomeGoals { get; set; }

        [JsonProperty("hbehinds")]
        public int? HomeBehinds { get; set; }

        [JsonProperty("hscore")]
        public int? HomeScore { get; set; }

        [JsonProperty("agoals")]
        public int? AwayGoals { get; set; }

        [JsonProperty("abehinds")]
        public int? AwayBehinds { get; set; }

        [JsonProperty("ascore")]
        public int? AwayScore { get; set; }

        [JsonIgnore]
        public DateTime DateUpdated { get; set; }
    }
}
