using AFLTips.Shared.Config;
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
    public class PlayerRepository : IPlayerRepository
    {
        private readonly SqlDbConfiguration _sqlConfig;

        public PlayerRepository(SqlDbConfiguration sqlConfig)
        {
            _sqlConfig = sqlConfig;
        }

        public List<Player> GetAllPlayers()
        {
            var players = new List<Player>();

            using (IDbConnection db = new SqlConnection(_sqlConfig.ConnectionString))
            {
                players = db.Query<Player>("[dbo].[uspPlayer_FetchAll]", commandType: CommandType.StoredProcedure).ToList();
            }

            return players;
        }
    }
}
