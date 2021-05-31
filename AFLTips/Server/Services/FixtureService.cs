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
        private readonly IMatchRepository _matchRepository;

        public FixtureService(IMatchRepository matchRepository, HttpClient httpClient)
        {
            _matchRepository = matchRepository;
            _httpClient = httpClient;
        }





        public async Task UpdateFixture()
        {
            var content = await _httpClient.GetStringAsync($"?q=games;year={DateTime.Today.Year}");

            var apiMatches = JsonConvert.DeserializeObject<Fixture>(content);

            await _matchRepository.UpsertMatches(apiMatches);
        }

        public Task<int> GetCurrentRound()
        {
            var allMatches =  _matchRepository.GetAll();

            return Task.FromResult(1);
        }
    }
}
