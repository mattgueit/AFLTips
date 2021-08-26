using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AFLTips.Server.Providers.Interfaces;
using AFLTips.Server.Repositories.Interfaces;
using AFLTips.Shared.Config;
using AFLTips.Shared.DataModels;
using Dapper;

namespace AFLTips.Server.Repositories
{
    public class FixtureRepository : IFixtureRepository
    {
        private readonly SqlDbConfiguration _sqlConfig;
        private readonly IDateTimeProvider _dateTimeProvider;

        public FixtureRepository(SqlDbConfiguration sqlConfig, IDateTimeProvider dateTimeProvider)
        {
            _sqlConfig = sqlConfig;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<AFLFixture> GetFixtureByYear(int year)
        {
            var fixture = new AFLFixture();
            var parameters = new DynamicParameters(new { Year = year });

            using (IDbConnection db = new SqlConnection(_sqlConfig.ConnectionString))
            {
                var matches = await db.QueryAsync<Match>("[dbo].[uspMatch_FetchByYear]", parameters, commandType: CommandType.StoredProcedure);
                fixture.Matches = matches.ToList();
            }

            return fixture;
        }

        public async Task<List<Match>> GetMatchesByRound(int roundId)
        {
            var matches = new List<Match>();
            var parameters = new DynamicParameters(new { RoundId = roundId });

            using (IDbConnection db = new SqlConnection(_sqlConfig.ConnectionString))
            {
                var result = await db.QueryAsync<Match>("dbo.[uspMatch_FetchByRound]", parameters, commandType: CommandType.StoredProcedure);
                matches = result.ToList();
            }

            return matches;
        }

        public async Task UpsertFixture(AFLFixture fixture)
        {
            var matches = fixture.Matches;

            var dataTable = new DataTable();
            dataTable.Columns.Add("MatchId", typeof(int));
            dataTable.Columns.Add("RoundId", typeof(int));
            dataTable.Columns.Add("HomeTeamId", typeof(int));
            dataTable.Columns.Add("AwayTeamId", typeof(int));
            dataTable.Columns.Add("MatchDate", typeof(DateTime));
            dataTable.Columns.Add("Venue", typeof(string));
            dataTable.Columns.Add("Completed", typeof(bool));
            dataTable.Columns.Add("WinnerTeamId", typeof(int));
            dataTable.Columns.Add("HomeGoals", typeof(int));
            dataTable.Columns.Add("HomeBehinds", typeof(int));
            dataTable.Columns.Add("HomeScore", typeof(int));
            dataTable.Columns.Add("AwayGoals", typeof(int));
            dataTable.Columns.Add("AwayBehinds", typeof(int));
            dataTable.Columns.Add("AwayScore", typeof(int));
            dataTable.Columns.Add("DateUpdated", typeof(DateTime));
            
            foreach(var match in matches)
            {
                dataTable.Rows.Add(
                    match.MatchId, 
                    match.RoundId, 
                    match.HomeTeamId, 
                    match.AwayTeamId, 
                    match.MatchDate,
                    match.Venue,
                    match.Completed,
                    match.WinnerTeamId,
                    match.HomeGoals,
                    match.HomeBehinds,
                    match.HomeScore,
                    match.AwayGoals,
                    match.AwayBehinds,
                    match.AwayScore,
                    _dateTimeProvider.DateTimeNow
                );;
            }

            using (IDbConnection db = new SqlConnection(_sqlConfig.ConnectionString))
            {
                await db.ExecuteAsync("[dbo].[uspMatch_Upsert]", new { Matches = dataTable.AsTableValuedParameter("dbo.udtMatchTableType") }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
