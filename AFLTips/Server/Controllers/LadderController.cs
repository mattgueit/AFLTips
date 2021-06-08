using System.Threading.Tasks;
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
        private readonly ILogger<LadderController> _logger;
        private readonly ILadderService _ladderService;

        public LadderController
        (
            ILogger<LadderController> logger,
            ILadderService ladderService
        )
        {
            _logger = logger;
            _ladderService = ladderService;
        }

        // api/ladder
        [HttpGet]
        public async Task<ActionResult<Ladder>> Get()
        {
            var ladder = await _ladderService.GetLadder();

            return Ok(ladder);
        }
    }
}
