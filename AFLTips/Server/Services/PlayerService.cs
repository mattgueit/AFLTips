using AFLTips.Server.Repositories;
using AFLTips.Shared.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AFLTips.Server.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public List<Player> GetAllPlayers()
        {
            return _playerRepository.GetAllPlayers();
        }
    }
}
