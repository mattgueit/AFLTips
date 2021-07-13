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
    public class LadderController : ControllerBase
    {
        private readonly ILadderService _ladderService;
        private readonly ExceptionHandler _exceptionHandler;

        public LadderController
        (
            ILadderService ladderService,
            ExceptionHandler exceptionHandler
        )
        {
            _ladderService = ladderService;
            _exceptionHandler = exceptionHandler;
        }

        // api/ladder
        [HttpGet]
        public Task<IActionResult> Get()
        {
            return _exceptionHandler.CatchExceptionsAsync(GetInternal);
        }

        private async Task<IActionResult> GetInternal()
        {
            var ladder = await _ladderService.GetLadder();

            return Ok(ladder);
        }

    }
}
