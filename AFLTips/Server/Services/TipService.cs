using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AFLTips.Shared.DataModels;
using AFLTips.Server.Repositories.Interfaces;
using AFLTips.Server.Services.Interfaces;
using AFLTips.Server.Providers.Interfaces;

namespace AFLTips.Server.Services
{
    public class TipService : ITipService
    {
        private readonly ITipRepository _tipRepository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public TipService(ITipRepository tipRepository, IDateTimeProvider dateTimeProvider)
        {
            _tipRepository = tipRepository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task UpdateTips(List<Tip> tips)
        {
            await _tipRepository.UpsertTips(tips);
        }

        public async Task<List<TippingScore>> GetTippingScores()
        {
            return await _tipRepository.FetchTippingScores(_dateTimeProvider.DateTimeNow.Year);
        }
    }
}
