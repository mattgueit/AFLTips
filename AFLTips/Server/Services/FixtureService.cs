using System;
using System.Linq;
using System.Threading.Tasks;
using AFLTips.Shared.DataModels;
using AFLTips.Shared.ViewModels;
using AFLTips.Server.Services.Interfaces;
using AFLTips.Server.Repositories.Interfaces;
using AFLTips.Server.Providers.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AFLTips.Server.Services
{
    public class FixtureService : IFixtureService
    {
        private readonly IFixtureRepository _fixtureRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IHttpProvider _httpHandler;
        private readonly IDateTimeProvider _dateTimeProvider;

        public FixtureService(
            IFixtureRepository fixtureRepository,
            ITeamRepository teamRepository,
            IHttpProvider httpHandler, 
            IDateTimeProvider dateTimeProvider
        )
        {
            _fixtureRepository = fixtureRepository;
            _teamRepository = teamRepository;
            _httpHandler = httpHandler;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<int> GetCurrentRound()
        {
            var fixture =  await _fixtureRepository.GetFixture();

            var roundId = fixture.Matches
                .Where(m => m.MatchDate >= _dateTimeProvider.DateTimeNow)
                .OrderBy(m => m.MatchDate)
                .Select(m => m.RoundId).FirstOrDefault();

            return roundId;
        }

        public async Task<List<GroupedMatches>> GetGroupedMatchesByRound(int roundId)
        {
            var matchDataModelsTask = _fixtureRepository.GetMatchesByRound(roundId);
            var teamsTask = _teamRepository.GetTeams();

            await Task.WhenAll(matchDataModelsTask, teamsTask);
            
            var matchDataModels = matchDataModelsTask.Result;
            var teams = teamsTask.Result;

            var matchesPerDate = GroupMatchesByDate(matchDataModels, teams);

            return matchesPerDate;
        }

        public async Task UpdateFixture()
        {
            var allGames = await _httpHandler.GetStringAsync($"?q=games;year={DateTime.Today.Year}");

            var fixture = JsonConvert.DeserializeObject<AFLFixture>(allGames);

            await _fixtureRepository.UpsertFixture(fixture);
        }

        private static List<GroupedMatches> GroupMatchesByDate(List<Match> matchDataModels, List<Team> teams)
        {
            var matchesGroupedByDate = matchDataModels
                .OrderBy(m => m.MatchDate)
                .GroupBy(m => m.MatchDate.Date)
                .ToList();

            var totalGroupedMatches = new List<GroupedMatches>();

            foreach (var matchesPerDate in matchesGroupedByDate)
            {
                var groupedMatches = new GroupedMatches()
                {
                    MatchDate = matchesPerDate.Key,
                    Matches = new List<MatchViewModel>()
                };

                foreach (var match in matchesPerDate)
                {
                    var homeTeamName = teams
                        .Where(t => t.TeamId == match.HomeTeamId)
                        .Select(t => t.TeamName)
                        .First();

                    var awayTeamName = teams
                        .Where(t => t.TeamId == match.AwayTeamId)
                        .Select(t => t.TeamName)
                        .First();

                    groupedMatches.Matches.Add(
                        new MatchViewModel()
                        {
                            MatchId = match.MatchId,
                            RoundId = match.RoundId,
                            HomeTeamId = match.HomeTeamId,
                            HomeTeamName = homeTeamName,
                            AwayTeamId = match.AwayTeamId,
                            AwayTeamName = awayTeamName,
                            MatchDate = match.MatchDate,
                            Venue = match.Venue,
                            Completed = match.Completed,
                            WinnerTeamId = match.WinnerTeamId,
                            HomeGoals = match.HomeGoals,
                            HomeBehinds = match.HomeBehinds,
                            HomeScore = match.HomeScore,
                            AwayGoals = match.AwayGoals,
                            AwayBehinds = match.AwayBehinds,
                            AwayScore = match.AwayScore
                        }
                    );
                }

                totalGroupedMatches.Add(groupedMatches);
            }

            return totalGroupedMatches;
        }
    }
}
