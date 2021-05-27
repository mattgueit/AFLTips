using System.Net.Http;
using System.Threading.Tasks;
using AFLTips.Shared.DataModels;
using Newtonsoft.Json;


namespace AFLTips.Server.Services
{
    public class LadderService : ILadderService
    {
        private readonly HttpClient _httpClient;

        public LadderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Ladder> GetLadder()
        {
            var content = await _httpClient.GetStringAsync("?q=standings");

            var ladder = JsonConvert.DeserializeObject<Ladder>(content);

            return ladder;
        }
    }
}
