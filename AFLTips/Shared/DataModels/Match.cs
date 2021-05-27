using System;

namespace AFLTips.Shared.DataModels
{
    public class Match
    {
        public int MatchId { get; set; }
        public int RoundId { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public DateTime MatchDate { get; set; }
        public string Venue { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
