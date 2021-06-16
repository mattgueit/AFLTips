using System;
using System.Net.Http;
using System.Threading.Tasks;
using AFLTips.Server.Handlers.Interfaces;

namespace AFLTips.Server.Handlers
{
    public class HttpClientHandler : IHttpHandler
    {
        private readonly HttpClient _httpClient;

        public HttpClientHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetStringAsync(string request)
        {
            return await _httpClient.GetStringAsync(request);
        }
    }
}
