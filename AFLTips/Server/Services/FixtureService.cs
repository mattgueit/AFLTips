using System;
using System.Linq;
using System.Threading.Tasks;
using AFLTips.Shared.DataModels;
using AFLTips.Server.Services.Interfaces;
using AFLTips.Server.Repositories.Interfaces;
using AFLTips.Server.Handlers.Interfaces;
using Newtonsoft.Json;

namespace AFLTips.Server.Services
{
    public class FixtureService : IFixtureService
    {
        private readonly IFixtureRepository _fixtureRepository;
        private readonly IHttpHandler _httpHandler;

        public FixtureService(IFixtureRepository fixtureRepository, IHttpHandler httpHandler)
        {
            _fixtureRepository = fixtureRepository;
            _httpHandler = httpHandler;
        }

        public async Task UpdateFixture()
        {
            var allGames = await _httpHandler.GetStringAsync($"?q=games;year={DateTime.Today.Year}");

            var fixture = JsonConvert.DeserializeObject<AFLFixture>(allGames);

            await _fixtureRepository.UpsertFixture(fixture);
        }

        public async Task<int> GetCurrentRound(DateTime dateTimeNow)
        {
            var fixture =  await _fixtureRepository.GetFixture();

            var roundId = fixture.Matches
                .Where(m => m.MatchDate >= dateTimeNow)
                .OrderBy(m => m.MatchDate)
                .Select(m => m.RoundId).First();

            return roundId;
        }
    }
}
