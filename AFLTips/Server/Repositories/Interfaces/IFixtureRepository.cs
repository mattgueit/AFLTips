using System.Collections.Generic;
using System.Threading.Tasks;
using AFLTips.Shared.DataModels;

namespace AFLTips.Server.Repositories.Interfaces
{
    public interface IFixtureRepository
    {
        Task<AFLFixture> GetFixtureByYear(int year);
        Task<List<Match>> GetMatchesByRound(int roundId);
        Task UpsertFixture(AFLFixture fixture);
    }
}   