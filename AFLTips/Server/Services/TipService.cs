using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AFLTips.Shared.DataModels;
using AFLTips.Server.Repositories.Interfaces;
using AFLTips.Server.Services.Interfaces;

namespace AFLTips.Server.Services
{
    public class TipService : ITipService
    {
        private readonly ITipRepository _tipRepository;

        public TipService(ITipRepository tipRepository)
        {
            _tipRepository = tipRepository;
        }

        public async Task UpdateTips(List<Tip> tips)
        {
            await _tipRepository.UpsertTips(tips);
        }

        public async Task<List<TippingScore>> GetTippingScores()
        {
            return await _tipRepository.FetchTippingScores(DateTime.Now.Year);
        }
    }
}
