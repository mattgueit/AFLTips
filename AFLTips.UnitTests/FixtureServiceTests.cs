using AFLTips.Server.Handlers.Interfaces;
using AFLTips.Server.Repositories.Interfaces;
using AFLTips.Server.Services;
using AFLTips.Server.Services.Interfaces;
using AFLTips.Shared.DataModels;
using AutoFixture;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AFLTips.UnitTests
{
    public class FixtureServiceTests
    {
        private readonly IFixtureService _sut;
        private readonly IFixtureRepository _fixtureRepository = Substitute.For<IFixtureRepository>();
        private readonly IHttpHandler _httpHandler = Substitute.For<IHttpHandler>();
        private readonly Fixture _fixture = new Fixture();

        public FixtureServiceTests()
        {
            _sut = new FixtureService(_fixtureRepository, _httpHandler);
        }

        [Fact]
        public async Task GetCurrentRound_WhenBetweenRounds_ReturnNextRound()
        {
            // Arrange
            var roundAndDatePairs = new List<RoundAndDatePair>();
            roundAndDatePairs.Add(new RoundAndDatePair(10, new DateTime(2021, 6, 5)));
            roundAndDatePairs.Add(new RoundAndDatePair(11, new DateTime(2021, 6, 12)));
            roundAndDatePairs.Add(new RoundAndDatePair(12, new DateTime(2021, 6, 19)));
            roundAndDatePairs.Add(new RoundAndDatePair(13, new DateTime(2021, 6, 26)));

            var fixture = GenerateFixture(roundAndDatePairs);
            _fixtureRepository
                .GetFixture()
                .Returns(fixture);

            var dateTimeNow = new DateTime(2021, 6, 15);

            // Act
            var currentRound = await _sut.GetCurrentRound(dateTimeNow);

            // Assert
            Assert.Equal(12, currentRound);
        }

        private AFLFixture GenerateFixture(List<RoundAndDatePair> roundAndDatePairs)
        {
            var matches = new List<Match>();
            foreach(var roundAndDatePair in roundAndDatePairs)
            {
                matches.Add(_fixture
                .Build<Match>()
                .With(x => x.RoundId, roundAndDatePair.RoundId)
                .With(x => x.MatchDate, roundAndDatePair.MatchDate)
                .Create());
            }

            var fixture = new AFLFixture() 
            { 
                Matches = matches 
            };
            return fixture;
        }

        private class RoundAndDatePair
        {
            public int RoundId { get; set; }
            public DateTime MatchDate { get; set; }

            public RoundAndDatePair(int roundId, DateTime matchDate)
            {
                RoundId = roundId;
                MatchDate = matchDate;
            }
        }



    }
}
