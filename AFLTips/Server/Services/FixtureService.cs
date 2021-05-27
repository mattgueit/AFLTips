using System.Net.Http;
using System.Threading.Tasks;
using AFLTips.Shared.DataModels;
using AFLTips.Server.Repositories;
using System.Collections.Generic;
using System;

namespace AFLTips.Server.Services
{
    public class FixtureService : IFixtureService
    {
        private readonly HttpClient _httpClient;
        private readonly IMatchRepository _matchRepository;

        public FixtureService(IMatchRepository matchRepository)
        {
            _httpClient = new HttpClient();
            _matchRepository = matchRepository;
            //_httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("https://github.com/mattgueit/AFLTips"));
        }

        public Task UpdateFixture()
        {
            var matches = new List<Match>();

            for (int i = 1; i <= 100; i++)
            {
                var match = new Match()
                {
                    MatchId = i,
                    RoundId = (int)Math.Ceiling(Decimal.Divide(i, 9)),
                    HomeTeamId = 1,
                    AwayTeamId = 2,
                    MatchDate = DateTime.Now,
                    Venue = "MCG",
                    DateUpdated = DateTime.Now
                };

                matches.Add(match);
            }

            return _matchRepository.UpsertMatches(matches);
        }

        public Task<int> GetCurrentRound()
        {
            var allMatches =  _matchRepository.GetAll();

            return Task.FromResult(1);
        }
    }
}
