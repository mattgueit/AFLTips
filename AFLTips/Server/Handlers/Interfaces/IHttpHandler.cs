using System.Threading.Tasks;

namespace AFLTips.Server.Handlers.Interfaces
{
    public interface IHttpHandler
    {
        Task<string> GetStringAsync(string request);
    }
}
