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

        public Task UpsertMatches(List<Match> matches)
        {
            var sqlParameter = new SqlParameter
            {
                ParameterName = "Matches",
                SqlDbType = SqlDbType.Structured,
                Value = matches,
                TypeName = "[dbo].[udtMatchTableType]"
            };

            var parameters = new
            {
                Matches = sqlParameter
            };

            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("default")))
            {
                db.Execute("[dbo].[uspMatch_Upsert]", param: parameters, commandType: CommandType.StoredProcedure);
            }

            return Task.CompletedTask;
        }
    }
}
