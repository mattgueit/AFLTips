using System;
using System.Threading.Tasks;

namespace AFLTips.Server.Services.Interfaces
{
    public interface IFixtureService
    {
        Task UpdateFixture();
        public Task<int> GetCurrentRound(DateTime dateTimeNow);
    }
}
