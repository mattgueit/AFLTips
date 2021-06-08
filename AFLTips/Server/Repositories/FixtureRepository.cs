using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AFLTips.Server.Repositories.Interfaces;
using AFLTips.Shared.Config;
using AFLTips.Shared.DataModels;
using Dapper;

namespace AFLTips.Server.Repositories
{
    public class FixtureRepository : IFixtureRepository
    {
        private readonly SqlDbConfiguration _sqlConfig;

        public FixtureRepository(SqlDbConfiguration sqlConfig)
        {
            _sqlConfig = sqlConfig;
        }

        public async Task<Fixture> GetFixture()
        {
            var fixture = new Fixture();

            using (IDbConnection db = new SqlConnection(_sqlConfig.ConnectionString))
            {
                var matches = await db.QueryAsync<Match>("[dbo].[uspMatch_FetchAll]", commandType: CommandType.StoredProcedure);
                fixture.Matches = matches.ToList();
            }

            return fixture;
        }

        public async Task UpsertFixture(Fixture fixture)
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
                    DateTime.Now
                );;
            }

            using (IDbConnection db = new SqlConnection(_sqlConfig.ConnectionString))
            {
                await db.ExecuteAsync("[dbo].[uspMatch_Upsert]", new { Matches = dataTable.AsTableValuedParameter("dbo.udtMatchTableType") }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
