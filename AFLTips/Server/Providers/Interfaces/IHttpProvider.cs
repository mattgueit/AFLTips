using System.Threading.Tasks;

namespace AFLTips.Server.Providers.Interfaces
{
    public interface IHttpProvider
    {
        Task<string> GetStringAsync(string request);
    }
}
