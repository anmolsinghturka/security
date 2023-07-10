using PicSecurityChecks.Shared;
using PicSecurityChecks.Client.Interfaces;
using System.Text.Json;
using System.Text;

namespace PicSecurityChecks.Client.Services
{
    public class SearchFlaggedNamesDataService : ISearchFlaggedNamesDataService
    {
        private readonly HttpClient _httpClient;

        public SearchFlaggedNamesDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<IEnumerable<PIC_FlaggedNames>> GetFlaggedNamesSearch(PIC_FlaggedNames pic_FlaggedName)
        {
            var flaggedNameJson = 
                new StringContent(JsonSerializer.Serialize(pic_FlaggedName), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/searchflaggednames", flaggedNameJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<IEnumerable<PIC_FlaggedNames>>
                    (await response.Content.ReadAsStreamAsync());
            }

            return null;
        }
    }
}
