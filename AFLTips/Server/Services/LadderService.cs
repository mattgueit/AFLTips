using System.Threading.Tasks;
using AFLTips.Server.Providers.Interfaces;
using AFLTips.Server.Services.Interfaces;
using AFLTips.Shared.DataModels;
using Newtonsoft.Json;


namespace AFLTips.Server.Services
{
    public class LadderService : ILadderService
    {
        private readonly IHttpProvider _httpHandler;

        public LadderService(IHttpProvider httpHandler)
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
