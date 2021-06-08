using System.Collections.Generic;
using AFLTips.Shared.DataModels;

namespace AFLTips.Server.Repositories.Interfaces
{
    public interface IPlayerRepository
    {
        List<Player> GetAllPlayers();
    }
}