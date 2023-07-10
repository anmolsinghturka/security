using PicSecurityChecks.Client.Interfaces;
using PicSecurityChecks.Shared;
using System.Text.Json;

namespace PicSecurityChecks.Client.Services
{
    public class NicheCallDataService : INicheCallsDataService
    {
        private readonly HttpClient _httpClient;

        public NicheCallDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<NicheReturnedData> GetNicheQuery(string lastName, string firstName)
        {
            NicheReturnedData data = new NicheReturnedData();
            return await JsonSerializer.DeserializeAsync<NicheReturnedData>
                (await _httpClient.GetStreamAsync($"api/Niche/{lastName}/{firstName}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<NicheReturnedData> GetOtherNiche(string path)
        {
            NicheReturnedData data = new NicheReturnedData();
            return await JsonSerializer.DeserializeAsync<NicheReturnedData>
                (await _httpClient.GetStreamAsync($"api/Niche/{path}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }); 
        }
    }
}
