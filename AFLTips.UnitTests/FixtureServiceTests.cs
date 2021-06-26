using AFLTips.Server.Providers.Interfaces;
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
        private readonly ITeamRepository _teamRepository = Substitute.For<ITeamRepository>();
        private readonly IHttpProvider _httpHandler = Substitute.For<IHttpProvider>();
        private readonly IDateTimeProvider _dateTimeProvider = Substitute.For<IDateTimeProvider>();
        private readonly Fixture _fixture = new Fixture();

        public FixtureServiceTests()
        {
            _sut = new FixtureService(_fixtureRepository, _teamRepository, _httpHandler, _dateTimeProvider);
        }

        [Fact]
        public async Task GetCurrentRound_WhenTheSeasonHasntStarted_ReturnFirstRound()
        {
            // Arrange
            var roundAndDatePairs = new List<RoundAndDatePair>();
            roundAndDatePairs.Add(new RoundAndDatePair(1, new DateTime(2021, 3, 6)));
            roundAndDatePairs.Add(new RoundAndDatePair(2, new DateTime(2021, 3, 13)));
            roundAndDatePairs.Add(new RoundAndDatePair(3, new DateTime(2021, 3, 20)));
            roundAndDatePairs.Add(new RoundAndDatePair(4, new DateTime(2021, 3, 27)));

            var fixture = GenerateAFLFixture(roundAndDatePairs);
            _fixtureRepository
                .GetFixture()
                .Returns(fixture);

            _dateTimeProvider.DateTimeNow.Returns(new DateTime(2021, 1, 1));

            // Act
            var currentRound = await _sut.GetCurrentRound();

            // Assert
            Assert.Equal(1, currentRound);
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

            var fixture = GenerateAFLFixture(roundAndDatePairs);
            _fixtureRepository
                .GetFixture()
                .Returns(fixture);

            _dateTimeProvider.DateTimeNow.Returns(new DateTime(2021, 6, 15));

            // Act
            var currentRound = await _sut.GetCurrentRound();

            // Assert
            Assert.Equal(12, currentRound);
        }

        [Fact]
        public async Task GetCurrentRound_WhenRoundIsInProgress_ReturnCurrentRound()
        {
            // Arrange
            var roundAndDatePairs = new List<RoundAndDatePair>();
            roundAndDatePairs.Add(new RoundAndDatePair(10, new DateTime(2021, 6, 5)));
            roundAndDatePairs.Add(new RoundAndDatePair(11, new DateTime(2021, 6, 12)));
            roundAndDatePairs.Add(new RoundAndDatePair(12, new DateTime(2021, 6, 19)));
            roundAndDatePairs.Add(new RoundAndDatePair(13, new DateTime(2021, 6, 26)));

            var fixture = GenerateAFLFixture(roundAndDatePairs);
            _fixtureRepository
                .GetFixture()
                .Returns(fixture);

            _dateTimeProvider.DateTimeNow.Returns(new DateTime(2021, 6, 26));

            // Act
            var currentRound = await _sut.GetCurrentRound();

            // Assert
            Assert.Equal(13, currentRound);
        }

        [Fact]
        public async Task GetCurrentRound_WhenSeasonIsOver_ReturnZero()
        {
            // Arrange
            var roundAndDatePairs = new List<RoundAndDatePair>();
            roundAndDatePairs.Add(new RoundAndDatePair(10, new DateTime(2021, 6, 5)));
            roundAndDatePairs.Add(new RoundAndDatePair(11, new DateTime(2021, 6, 12)));
            roundAndDatePairs.Add(new RoundAndDatePair(12, new DateTime(2021, 6, 19)));
            roundAndDatePairs.Add(new RoundAndDatePair(13, new DateTime(2021, 6, 26)));

            var fixture = GenerateAFLFixture(roundAndDatePairs);
            _fixtureRepository
                .GetFixture()
                .Returns(fixture);

            _dateTimeProvider.DateTimeNow.Returns(new DateTime(2021, 11, 22));

            // Act
            var currentRound = await _sut.GetCurrentRound();

            // Assert
            Assert.Equal(0, currentRound);
        }

        private AFLFixture GenerateAFLFixture(List<RoundAndDatePair> roundAndDatePairs)
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
