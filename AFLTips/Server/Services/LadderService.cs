using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AFLTips.Shared.DataModels;
using Newtonsoft.Json;

namespace AFLTips.Server.Services
{
    public class LadderService : ILadderService
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public async Task<Ladder> GetLadder()
        {
            var content = await _httpClient.GetStringAsync("https://api.squiggle.com.au/?q=standings");
            var ladder = new Ladder();

            try 
            { 
                ladder = JsonConvert.DeserializeObject<Ladder>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return ladder;
        }
    }
}
