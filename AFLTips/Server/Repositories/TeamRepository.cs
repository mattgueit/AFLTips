using System.Collections.Generic;
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
    public class TeamRepository : ITeamRepository
    {
        private readonly SqlDbConfiguration _sqlConfig;

        public TeamRepository(SqlDbConfiguration sqlConfig)
        {
            _sqlConfig = sqlConfig;
        }

        public async Task<List<Team>> GetTeams()
        {
            var teams = new List<Team>();

            using (IDbConnection db = new SqlConnection(_sqlConfig.ConnectionString))
            {
                var result = await db.QueryAsync<Team>("[dbo].[uspTeam_FetchAll]", commandType: CommandType.StoredProcedure);
                teams = result.ToList();
            }

            return teams;
        }
    }
}