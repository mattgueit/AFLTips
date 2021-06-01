using AFLTips.Shared.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AFLTips.Server.Repositories
{
    public interface IFixtureRepository
    {
        Task<Fixture> GetFixture();
        Task UpsertFixture(Fixture fixture);
    }
}   