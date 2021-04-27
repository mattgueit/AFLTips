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
        private readonly IConfiguration _configuration;

        public PlayerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Player> GetAllPlayers()
        {
            var players = new List<Player>();

            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("default")))
            {
                players = db.Query<Player>("[dbo].[uspPlayer_FetchAll]", commandType: CommandType.StoredProcedure).ToList();
            }

            return players;
        }
    }
}
