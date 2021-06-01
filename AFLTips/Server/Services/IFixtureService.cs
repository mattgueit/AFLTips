using System.Collections.Generic;
using System.Threading.Tasks;
using AFLTips.Shared.DataModels;

namespace AFLTips.Server.Services
{
    public interface IFixtureService
    {
        Task Update();
        public Task<int> GetCurrentRound();
    }
}
