using System.Net.Http;
using System.Threading.Tasks;
using AFLTips.Shared.DataModels;
using AFLTips.Server.Repositories;
using System.Collections.Generic;
using System;
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

        public Task<int> GetCurrentRound()
        {
            var fixture =  _matchRepository.GetFixture();

            return Task.FromResult(1);
        }
    }
}
