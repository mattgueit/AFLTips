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
        [HttpGet("update")]
        public async Task UpdateTips()
        {
            var tips = new List<Tip>();
            tips.Add(new Tip() { MatchId = 6345, PlayerId = 2, TeamId = 11});

            await _tipService.UpdateTips(tips);
        }
    }
}