using AFLTips.Shared.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AFLTips.Server.Repositories
{
    public interface IFixtureRepository
    {
        Fixture GetFixture();
        Task UpsertFixture(Fixture fixture);
    }
}   