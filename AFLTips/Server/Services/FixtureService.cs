using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AFLTips.Shared.DataModels;
using AFLTips.Server.Repositories;
using Newtonsoft.Json;

namespace AFLTips.Server.Services
{
    public class FixtureService : IFixtureService
    {
        private readonly HttpClient _httpClient;
        private readonly IFixtureRepository _matchRepository;

        public FixtureService(IFixtureRepository matchRepository, HttpClient httpClient)
        {
            _matchRepository = matchRepository;
            _httpClient = httpClient;
        }

        public async Task Update()
        {
            var allGames = await _httpClient.GetStringAsync($"?q=games;year={DateTime.Today.Year}");

            var fixture = JsonConvert.DeserializeObject<Fixture>(allGames);

            await _matchRepository.UpsertFixture(fixture);
        }

        public async Task<int> GetCurrentRound()
        {
            var fixture =  await _matchRepository.GetFixture();

            var roundId = fixture.Matches
                .Where(m => m.MatchDate >= DateTime.Now)
                .OrderBy(m => m.MatchDate)
                .Select(m => m.RoundId).First();

            return roundId;
        }
    }
}
