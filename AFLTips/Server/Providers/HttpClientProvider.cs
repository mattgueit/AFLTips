using System;
using System.Net.Http;
using System.Threading.Tasks;
using AFLTips.Server.Providers.Interfaces;

namespace AFLTips.Server.Providers
{
    public class HttpClientProvider : IHttpProvider
    {
        private readonly HttpClient _httpClient;

        public HttpClientProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetStringAsync(string request)
        {
            return await _httpClient.GetStringAsync(request);
        }
    }
}
