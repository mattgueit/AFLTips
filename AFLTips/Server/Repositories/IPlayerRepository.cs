using AFLTips.Shared.DataModels;
using System.Collections.Generic;

namespace AFLTips.Server.Repositories
{
    public interface IPlayerRepository
    {
        List<Player> GetAllPlayers();
    }
}