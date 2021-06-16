using System;
using System.Linq;
using System.Threading.Tasks;
using AFLTips.Shared.DataModels;
using AFLTips.Server.Services.Interfaces;
using AFLTips.Server.Repositories.Interfaces;
using AFLTips.Server.Providers.Interfaces;
using Newtonsoft.Json;

namespace AFLTips.Server.Services
{
    public class FixtureService : IFixtureService
    {
        private readonly IFixtureRepository _fixtureRepository;
        private readonly IHttpProvider _httpHandler;
        private readonly IDateTimeProvider _dateTimeProvider;

        public FixtureService(
            IFixtureRepository fixtureRepository, 
            IHttpProvider httpHandler, 
            IDateTimeProvider dateTimeProvider
        )
        {
            _fixtureRepository = fixtureRepository;
            _httpHandler = httpHandler;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task UpdateFixture()
        {
            var allGames = await _httpHandler.GetStringAsync($"?q=games;year={DateTime.Today.Year}");

            var fixture = JsonConvert.DeserializeObject<AFLFixture>(allGames);

            await _fixtureRepository.UpsertFixture(fixture);
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
    }
}
