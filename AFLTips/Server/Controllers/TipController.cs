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
    public class TipController : ControllerBase
    {
        private readonly ILogger<TipController> _logger;
        private readonly ITipService _tipService;

        public TipController
        (
            ILogger<TipController> logger,
            ITipService tipService
        )
        {
            _logger = logger;
            _tipService = tipService;
        }

        // api/tip
        [HttpPut("update")]
        public async Task<IActionResult> UpdateTips(List<Tip> tips)
        {
            await _tipService.UpdateTips(tips);

            return Ok();
        }

        // api/tip/scores
        [HttpGet("scores")]
        public async Task<ActionResult<List<TippingScore>>> GetTippingScores()
        {
            var overallScores = await _tipService.GetTippingScores();

            return Ok(overallScores);
        }

    }
}