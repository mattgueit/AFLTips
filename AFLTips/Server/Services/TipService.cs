using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AFLTips.Shared.DataModels;
using AFLTips.Server.Repositories.Interfaces;
using AFLTips.Server.Services.Interfaces;

namespace AFLTips.Server.Services
{
    public class TipService : ITipService
    {
        private readonly HttpClient _httpClient;
        private readonly ITipRepository _tipRepository;

        public TipService(ITipRepository tipRepository, HttpClient httpClient)
        {
            _tipRepository = tipRepository;
            _httpClient = httpClient;
        }

        public async Task UpdateTips(List<Tip> tips)
        {
            await _tipRepository.UpsertTips(tips);
        }
    }
}
