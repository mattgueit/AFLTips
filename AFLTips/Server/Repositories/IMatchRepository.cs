using AFLTips.Shared.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AFLTips.Server.Repositories
{
    public interface IMatchRepository
    {
        List<Match> GetAll();
        Task UpsertMatches(List<Match> matches);
    }
}   