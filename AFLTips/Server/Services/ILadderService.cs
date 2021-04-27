using System.Collections.Generic;
using System.Threading.Tasks;
using AFLTips.Shared.DataModels;

namespace AFLTips.Server.Services
{
    public interface ILadderService
    {
        public Task<Ladder> GetLadder();
    }
}
