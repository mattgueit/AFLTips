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
    public class TipController : ControllerBase
    {
        private readonly ITipService _tipService;
        private readonly ExceptionHandler _exceptionHandler;

        public TipController
        (
            ITipService tipService,
            ExceptionHandler exceptionHandler
        )
        {
            _tipService = tipService;
            _exceptionHandler = exceptionHandler;
        }

        // api/tip
        [HttpPut("update")]
        public Task<IActionResult> UpdateTips(List<Tip> tips)
        {
            return _exceptionHandler.CatchExceptionsAsync(UpdateTipsInternal, tips);
        }

        private async Task<IActionResult> UpdateTipsInternal(List<Tip> tips)
        {
            await _tipService.UpdateTips(tips);

            return Ok();
        }

        // api/tip/scores
        [HttpGet("scores")]
        public Task<IActionResult> GetTippingScores()
        {
            return _exceptionHandler.CatchExceptionsAsync(GetTippingScoresInternal);
        }

        private async Task<IActionResult> GetTippingScoresInternal()
        {
            var overallScores = await _tipService.GetTippingScores();

            return Ok(overallScores);
        }

    }
}