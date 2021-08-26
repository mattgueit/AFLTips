using System;

namespace AFLTips.Shared.ViewModels
{
    public class MatchViewModel
    {
        public int MatchId { get; set; }
        public int RoundId { get; set; }
        public int? HomeTeamId { get; set; }
        public string HomeTeamName { get; set; }
        public int? AwayTeamId { get; set; }
        public string AwayTeamName { get; set; }
        public DateTime MatchDate { get; set; }
        public string Venue { get; set; }
        public bool Completed { get; set; }
        public int? WinnerTeamId { get; set; }
        public int? HomeGoals { get; set; }
        public int? HomeBehinds { get; set; }
        public int? HomeScore { get; set; }
        public int? AwayGoals { get; set; }
        public int? AwayBehinds { get; set; }
        public int? AwayScore { get; set; }
    }
}
