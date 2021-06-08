using System.Collections.Generic;
using System.Threading.Tasks;
using AFLTips.Shared.DataModels;

namespace AFLTips.Server.Repositories.Interfaces
{
    public interface ITipRepository
    {
        Task UpsertTips(List<Tip> tips);
    }
}   