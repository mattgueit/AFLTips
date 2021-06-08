using System.Collections.Generic;
using System.Threading.Tasks;
using AFLTips.Server.Repositories.Interfaces;
using AFLTips.Server.Services.Interfaces;
using AFLTips.Shared.DataModels;

namespace AFLTips.Server.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<List<Player>> GetAllPlayers()
        {
            return await _playerRepository.GetAllPlayers();
        }
    }
}
