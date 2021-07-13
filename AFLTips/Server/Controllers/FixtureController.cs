using System;
using System.Threading.Tasks;
using AFLTips.Server.Handlers;
using AFLTips.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AFLTips.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FixtureController : ControllerBase
    {
        private readonly IFixtureService _fixtureService;
        private readonly ExceptionHandler _exceptionHandler;

        public FixtureController
        (
            IFixtureService fixtureService,
            ExceptionHandler exceptionHandler
        )
        {
            _fixtureService = fixtureService;
            _exceptionHandler = exceptionHandler;
        }

        // api/fixture/currentRound
        [HttpGet("currentRound")]
        public Task<IActionResult> GetCurrentRound()
        {
            return _exceptionHandler.CatchExceptionsAsync(GetCurrentRoundInternal);

        }

        private async Task<IActionResult> GetCurrentRoundInternal()
        {
            var currentRoundId = await _fixtureService.GetCurrentRound();

            return Ok(currentRoundId);
        }

        // api/fixture/groupedMatchesByRound
        [HttpGet("groupedMatchesByRound")]
        public Task<IActionResult> GetGroupedMatchesByRound(int roundId)
        {
            return _exceptionHandler.CatchExceptionsAsync(GetGroupedMatchesByRoundInternal, roundId);
        }

        private async Task<IActionResult> GetGroupedMatchesByRoundInternal(int roundId)
        {
            var matches = await _fixtureService.GetGroupedMatchesByRound(roundId);

            return Ok(matches);
        }

        // api/fixture/update
        [HttpGet("update")]
        public Task<IActionResult> UpdateFixture()
        {
            return _exceptionHandler.CatchExceptionsAsync(UpdateFixtureInternal);
        }

        private async Task<IActionResult> UpdateFixtureInternal()
        {
            await _fixtureService.UpdateFixture();

            return Ok();
        }
    }
}