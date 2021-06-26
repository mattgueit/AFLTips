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

        public async Task<List<MatchViewModel>> GetMatchesByRound(int roundId)
        {
            var matchViewModels = new List<MatchViewModel>();
            var matchDataModels = await _fixtureRepository.GetMatchesByRound(roundId);
            var teams = await _teamRepository.GetTeams();

            foreach (var matchDataModel in matchDataModels)
            {
                var homeTeamName = teams
                    .Where(t => t.TeamId == matchDataModel.HomeTeamId)
                    .Select(t => t.TeamName)
                    .First();

                var awayTeamName = teams
                    .Where(t => t.TeamId == matchDataModel.AwayTeamId)
                    .Select(t => t.TeamName)
                    .First();

                matchViewModels.Add
                (
                    new MatchViewModel()
                    {
                        MatchId = matchDataModel.MatchId,
                        RoundId = matchDataModel.RoundId,
                        HomeTeamId = matchDataModel.HomeTeamId,
                        HomeTeamName = homeTeamName,
                        AwayTeamId = matchDataModel.AwayTeamId,
                        AwayTeamName = awayTeamName,
                        MatchDate = matchDataModel.MatchDate,
                        Venue = matchDataModel.Venue,
                        Completed = matchDataModel.Completed,
                        WinnerTeamId = matchDataModel.WinnerTeamId,
                        HomeGoals = matchDataModel.HomeGoals,
                        HomeBehinds = matchDataModel.HomeBehinds,
                        HomeScore = matchDataModel.HomeScore,
                        AwayGoals = matchDataModel.AwayGoals,
                        AwayBehinds = matchDataModel.AwayBehinds,
                        AwayScore = matchDataModel.AwayScore
                    }
                );
            }

            return matchViewModels;
        }

        public async Task UpdateFixture()
        {
            var allGames = await _httpHandler.GetStringAsync($"?q=games;year={DateTime.Today.Year}");

            var fixture = JsonConvert.DeserializeObject<AFLFixture>(allGames);

            await _fixtureRepository.UpsertFixture(fixture);
        }
    }
}
