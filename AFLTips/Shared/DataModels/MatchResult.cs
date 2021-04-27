namespace AFLTips.Shared.DataModels
{
    public class MatchResult
    {
        public int MatchId { get; set; }
        public int WinnerTeamId { get; set; }
        public int HomeGoals { get; set; }
        public int HomeBehinds { get; set; }
        public int HomeScore { get; set; }
        public int AwayGoals { get; set; }
        public int AwayBehinds { get; set; }
        public int AwayScore { get; set; }
    }
}
