using AFLTips.Shared.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AFLTips.Server.Services.Interfaces
{
    public interface IPlayerService
    {
        Task<List<Player>> GetAllPlayers();
    }
}
