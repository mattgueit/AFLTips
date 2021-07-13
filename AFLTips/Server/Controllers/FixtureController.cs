using System;
using System.Threading.Tasks;
using AFLTips.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AFLTips.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FixtureController : ControllerBase
    {
        private readonly ILogger<FixtureController> _logger;
        private readonly IFixtureService _fixtureService;

        public FixtureController
        (
            ILogger<FixtureController> logger,
            IFixtureService fixtureService
        )
        {
            _logger = logger;
            _fixtureService = fixtureService;
        }

        // api/fixture/currentRound
        [HttpGet("currentRound")]
        public async Task<IActionResult> GetCurrentRound()
        {
            var dateTimeNow = DateTime.Now;
            var currentRoundId = await _fixtureService.GetCurrentRound();

            return Ok(currentRoundId);
        }

        // api/fixture/groupedMatchesByRound
        [HttpGet("groupedMatchesByRound")]
        public async Task<IActionResult> GetGroupedMatchesByRound(int roundId)
        {
            var matches = await _fixtureService.GetGroupedMatchesByRound(roundId);

            return Ok(matches);
        }


        // api/fixture/update
        [HttpGet("update")]
        public async Task<IActionResult> UpdateFixture()
        {
            await _fixtureService.UpdateFixture();

            return Ok();
        }
    }
}