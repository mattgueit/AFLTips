using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AFLTips.Shared.DataModels
{
    public class LadderPosition
    {
        [JsonProperty("id")]
        public int TeamId { get; set; }

        [JsonProperty("name")]
        public string TeamName { get; set; }

        [JsonProperty("rank")]
        public int Rank { get; set; }

        [JsonProperty("wins")]
        public int Wins { get; set; }

        [JsonProperty("draws")]
        public int Draws { get; set; }

        [JsonProperty("losses")]
        public int Losses { get; set; }

        [JsonProperty("pts")]
        public int Points { get; set; }

        [JsonProperty("percentage")]
        public decimal Percentage { get; set; }

        [JsonProperty("played")]
        public int GamesPlayed { get; set; }

        [JsonProperty("goals_for")]
        public int GoalsFor { get; set; }

        [JsonProperty("goals_against")]
        public int GoalsAgainst { get; set; }

        [JsonProperty("behinds_for")]
        public int BehindsFor { get; set; }

        [JsonProperty("behinds_against")]
        public int BehindsAgainst { get; set; }

        [JsonProperty("points_for")]
        public int PointsFor { get; set; }

        [JsonProperty("points_against")]
        public int PointsAgainst { get; set; }
    }
}
