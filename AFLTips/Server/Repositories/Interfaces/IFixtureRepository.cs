using System.Threading.Tasks;
using AFLTips.Shared.DataModels;

namespace AFLTips.Server.Repositories.Interfaces
{
    public interface IFixtureRepository
    {
        Task<AFLFixture> GetFixture();
        Task UpsertFixture(AFLFixture fixture);
    }
}   