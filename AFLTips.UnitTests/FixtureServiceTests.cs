using AFLTips.Server.Providers.Interfaces;
using AFLTips.Server.Repositories.Interfaces;
using AFLTips.Server.Services;
using AFLTips.Server.Services.Interfaces;
using AFLTips.Shared.DataModels;
using AutoFixture;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
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
                .GetFixtureByYear(Arg.Any<int>())
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
                .GetFixtureByYear(Arg.Any<int>())
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
                .GetFixtureByYear(Arg.Any<int>())
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
                .GetFixtureByYear(Arg.Any<int>())
                .Returns(fixture);

            _dateTimeProvider.DateTimeNow.Returns(new DateTime(2021, 11, 22));

            // Act
            var currentRound = await _sut.GetCurrentRound();

            // Assert
            Assert.Equal(0, currentRound);
        }

        [Fact]
        public async Task GetGroupedMatchesByRound_WhenCalled_GroupMatchesByDate()
        {
            // Arrange
            var matches = new List<Match>()
            {
                new Match() { MatchId = 1, RoundId = 10, MatchDate = new DateTime(2021, 06, 10), HomeTeamId = 1, AwayTeamId = 2 },
                new Match() { MatchId = 2, RoundId = 10, MatchDate = new DateTime(2021, 06, 11), HomeTeamId = 3, AwayTeamId = 4 },
                new Match() { MatchId = 3, RoundId = 10, MatchDate = new DateTime(2021, 06, 11), HomeTeamId = 5, AwayTeamId = 6 },
                new Match() { MatchId = 4, RoundId = 10, MatchDate = new DateTime(2021, 06, 12), HomeTeamId = 7, AwayTeamId = 8 }
            };
            _fixtureRepository
                .GetMatchesByRound(Arg.Any<int>())
                .Returns(matches);

            var teams = GenerateTeams();
            _teamRepository
                .GetTeams()
                .Returns(teams);

            // Act
            var groupedMatchesByRound = await _sut.GetGroupedMatchesByRound(10);

            // Assert
            var firstDayMatchCount = groupedMatchesByRound.FirstOrDefault(g => g.MatchDate == new DateTime(2021, 06, 10)).Matches.Count();
            Assert.Equal(1, firstDayMatchCount);

            var secondDayMatchCount = groupedMatchesByRound.FirstOrDefault(g => g.MatchDate == new DateTime(2021, 06, 11)).Matches.Count();
            Assert.Equal(2, secondDayMatchCount);

            var thirdDayMatchCount = groupedMatchesByRound.FirstOrDefault(g => g.MatchDate == new DateTime(2021, 06, 12)).Matches.Count();
            Assert.Equal(1, thirdDayMatchCount);

            var totalDayCount = groupedMatchesByRound.Count;
            Assert.Equal(3, totalDayCount);
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

        private List<Team> GenerateTeams()
        {
            var teams = new List<Team>()
            {
                new Team() { TeamId = 1,  TeamName = "Adelaide" },
                new Team() { TeamId = 2,  TeamName = "Brisbane Lions" },
                new Team() { TeamId = 3,  TeamName = "Carlton" },
                new Team() { TeamId = 4,  TeamName = "Collingwood" },
                new Team() { TeamId = 5,  TeamName = "Essendon" },
                new Team() { TeamId = 6,  TeamName = "Fremantle" },
                new Team() { TeamId = 7,  TeamName = "Geelong" },
                new Team() { TeamId = 8,  TeamName = "Gold Coast" },
                new Team() { TeamId = 9,  TeamName = "Greater Western Sydney" },
                new Team() { TeamId = 10, TeamName = "Hawthorn" },
                new Team() { TeamId = 11, TeamName = "Melbourne" },
                new Team() { TeamId = 12, TeamName = "North Melbourne" },
                new Team() { TeamId = 13, TeamName = "Port Adelaide" },
                new Team() { TeamId = 14, TeamName = "Richmond" },
                new Team() { TeamId = 15, TeamName = "St Kilda" },
                new Team() { TeamId = 16, TeamName = "Sydney" },
                new Team() { TeamId = 17, TeamName = "West Coast" },
                new Team() { TeamId = 18, TeamName = "Western Bulldogs" }
            };

            return teams;
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
