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
    public class TipRepository : ITipRepository
    {
        private readonly SqlDbConfiguration _sqlConfig;

        public TipRepository(SqlDbConfiguration sqlConfig)
        {
            _sqlConfig = sqlConfig;
        }

        public async Task UpsertTips(List<Tip> tips)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("MatchId", typeof(int));
            dataTable.Columns.Add("PlayerId", typeof(int));
            dataTable.Columns.Add("TeamId", typeof(int));
            
            foreach(var tip in tips)
            {
                dataTable.Rows.Add(
                    tip.MatchId,
                    tip.PlayerId,
                    tip.TeamId
                );;
            }

            using (IDbConnection db = new SqlConnection(_sqlConfig.ConnectionString))
            {
                await db.ExecuteAsync("[dbo].[uspTip_Upsert]", new { Tips = dataTable.AsTableValuedParameter("dbo.udtTipTableType") }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<List<TippingScore>> FetchTippingScores(int year)
        {
            var parameters = new DynamicParameters(new { Year = year});
            using (IDbConnection db = new SqlConnection(_sqlConfig.ConnectionString))
            {
                var overallScores = await db.QueryAsync<TippingScore>("dbo.[uspTip_FetchScores]", parameters, commandType: CommandType.StoredProcedure);

                return overallScores.ToList();
            }
        }
    }
}
