using System.Threading.Tasks;
using AFLTips.Shared.DataModels;

namespace AFLTips.Server.Services.Interfaces
{
    public interface ILadderService
    {
        public Task<Ladder> GetLadder();
    }
}
