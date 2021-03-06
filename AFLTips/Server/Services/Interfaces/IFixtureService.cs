using System.Collections.Generic;
using System.Threading.Tasks;
using AFLTips.Shared.ViewModels;

namespace AFLTips.Server.Services.Interfaces
{
    public interface IFixtureService
    {
        Task UpdateFixture();
        public Task<List<GroupedMatches>> GetGroupedMatchesByRound(int roundId);
        public Task<int> GetCurrentRound();
    }
}
