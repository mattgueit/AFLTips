using AFLTips.Shared.DataModels;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AFLTips.Server.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        private readonly IConfiguration _configuration;

        public MatchRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Match> GetAll()
        {
            var matches = new List<Match>();

            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("default")))
            {
                matches = db.Query<Match>("[dbo].[uspMatch_FetchAll]", commandType: CommandType.StoredProcedure).ToList();
            }

            return matches;
        }

        public async Task UpsertMatches(Fixture fixture)
        {
            var matches = fixture.Matches;

            var dataTable = new DataTable();
            dataTable.Columns.Add("MatchId", typeof(int));
            dataTable.Columns.Add("RoundId", typeof(int));
            dataTable.Columns.Add("HomeTeamId", typeof(int));
            dataTable.Columns.Add("AwayTeamId", typeof(int));
            dataTable.Columns.Add("MatchDate", typeof(DateTime));
            dataTable.Columns.Add("Venue", typeof(string));
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
                    DateTime.Now
                );
            }

            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("default")))
            {
                await db.ExecuteAsync("[dbo].[uspMatch_Upsert]", new { Matches = dataTable.AsTableValuedParameter("dbo.udtMatchTableType") }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
