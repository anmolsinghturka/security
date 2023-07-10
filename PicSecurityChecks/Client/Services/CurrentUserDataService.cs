using PicSecurityChecks.Client.Interfaces;
using PicSecurityChecks.Shared;
using System.Text.Json;

namespace PicSecurityChecks.Client.Services
{
    public class CurrentUserDataService : ICurrentUserDataService
    {
        private readonly HttpClient _httpClient;

        public CurrentUserDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetCurrentUser()
        {
            return await JsonSerializer.DeserializeAsync<string>
                (await _httpClient.GetStreamAsync($"api/currentperson"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
