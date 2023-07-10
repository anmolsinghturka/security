using PicSecurityChecks.Client.Interfaces;
using PicSecurityChecks.Shared;
using System.Text.Json;

namespace PicSecurityChecks.Client.Services
{
    public class InetDataService : IInetDataService
    {
        private readonly HttpClient _httpClient;

        public InetDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Inet>> GetInet(string firstName, string lastName)
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Inet>>
                (await _httpClient.GetStreamAsync($"api/inet/{firstName}/{lastName}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
