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
    public class PlayerRepository : IPlayerRepository
    {
        private readonly SqlDbConfiguration _sqlConfig;

        public PlayerRepository(SqlDbConfiguration sqlConfig)
        {
            _sqlConfig = sqlConfig;
        }

        public async Task<List<Player>> GetAllPlayers()
        {
            using (IDbConnection db = new SqlConnection(_sqlConfig.ConnectionString))
            {
                var players = await db.QueryAsync<Player>("[dbo].[uspPlayer_FetchAll]", commandType: CommandType.StoredProcedure);

                return players.ToList();
            }
        }
    }
}
