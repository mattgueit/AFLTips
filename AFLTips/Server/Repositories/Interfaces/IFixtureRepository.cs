using System.Threading.Tasks;
using AFLTips.Shared.DataModels;

namespace AFLTips.Server.Repositories.Interfaces
{
    public interface IFixtureRepository
    {
        Task<Fixture> GetFixture();
        Task UpsertFixture(Fixture fixture);
    }
}   