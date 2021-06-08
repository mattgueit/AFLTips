using System.Collections.Generic;
using System.Threading.Tasks;
using AFLTips.Shared.DataModels;

namespace AFLTips.Server.Repositories.Interfaces
{
    public interface IPlayerRepository
    {
        Task<List<Player>> GetAllPlayers();
    }
}