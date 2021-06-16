using System.Threading.Tasks;
using AFLTips.Server.Handlers.Interfaces;
using AFLTips.Server.Services.Interfaces;
using AFLTips.Shared.DataModels;
using Newtonsoft.Json;


namespace AFLTips.Server.Services
{
    public class LadderService : ILadderService
    {
        private readonly IHttpHandler _httpHandler;

        public LadderService(IHttpHandler httpHandler)
        {
            _httpHandler = httpHandler;
        }

        public async Task<Ladder> GetLadder()
        {
            var content = await _httpHandler.GetStringAsync("?q=standings");

            var ladder = JsonConvert.DeserializeObject<Ladder>(content);

            return ladder;
        }
    }
}
