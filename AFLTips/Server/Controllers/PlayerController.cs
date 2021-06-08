using System.Collections.Generic;
using System.Threading.Tasks;
using AFLTips.Server.Services.Interfaces;
using AFLTips.Shared.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AFLTips.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly ILogger<PlayerController> _logger;
        private readonly IPlayerService _playerService;

        public PlayerController
        (
            ILogger<PlayerController> logger,
            IPlayerService playerService
        )
        {
            _logger = logger;
            _playerService = playerService;
        }

        // api/player
        [HttpGet]
        public async Task<ActionResult<List<Player>>> Get()
        {
            var players = await _playerService.GetAllPlayers();

            return Ok(players);
        }
    }
}
