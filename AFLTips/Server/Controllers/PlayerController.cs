using System.Collections.Generic;
using System.Threading.Tasks;
using AFLTips.Server.Handlers;
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
        private readonly IPlayerService _playerService;
        private readonly ExceptionHandler _exceptionHandler;

        public PlayerController
        (
            IPlayerService playerService,
            ExceptionHandler exceptionHandler
        )
        {
            _playerService = playerService;
            _exceptionHandler = exceptionHandler;
        }

        // api/player
        [HttpGet]
        public Task<IActionResult> Get()
        {
            return _exceptionHandler.CatchExceptionsAsync(GetInternal);
        }

        private async Task<IActionResult> GetInternal()
        {
            var players = await _playerService.GetAllPlayers();

            return Ok(players);
        }
    }
}
