using System.Collections.Generic;
using System.Threading.Tasks;
using AFLTips.Shared.DataModels;

namespace AFLTips.Server.Services.Interfaces
{
    public interface ITipService
    {
        Task UpdateTips(List<Tip> tips);
        Task<List<TippingScore>> GetTippingScores();
    }
}
