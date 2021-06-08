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

        // api/fixture
        [HttpGet("currentRound")]
        public async Task<int> GetCurrentRound()
        {
            return await _fixtureService.GetCurrentRound();
        }

        // api/fixture/update
        [HttpGet("update")]
        public async Task UpdateFixture()
        {
            await _fixtureService.UpdateFixture();
        }
    }
}