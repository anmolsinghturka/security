using PicSecurityChecks.Shared;
using PicSecurityChecks.Client.Interfaces;
using System.Text.Json;
using System.Text;

namespace PicSecurityChecks.Client.Services
{
    public class FlaggedNamesDataService : IFlaggedNamesDataService
    {
        private readonly HttpClient _httpClient;

        public FlaggedNamesDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<PIC_FlaggedNames>> GetPIC_FlaggedNames()
        {
           IEnumerable<PIC_FlaggedNames> pIC_FlaggedNames = null;
            pIC_FlaggedNames = await JsonSerializer.DeserializeAsync<IEnumerable<PIC_FlaggedNames>>
                (await _httpClient.GetStreamAsync($"api/flaggednames"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            if (pIC_FlaggedNames == null)
                pIC_FlaggedNames = new List<PIC_FlaggedNames>();

            return pIC_FlaggedNames;
        }

         public async Task<PIC_FlaggedNames> GetFlaggedNamesById(int id)
        {
            PIC_FlaggedNames pIC_FlaggedNames = null;
            pIC_FlaggedNames = await JsonSerializer.DeserializeAsync<PIC_FlaggedNames>
                (await _httpClient.GetStreamAsync($"api/flaggednames/{id}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive= true });

            if (pIC_FlaggedNames == null)
                pIC_FlaggedNames = new PIC_FlaggedNames();

            return pIC_FlaggedNames;
        }

        public async Task<int> CheckFlaggedNames(string FirstName, string LastName, string dob)
        {
            return await JsonSerializer.DeserializeAsync<int>
                (await _httpClient.GetStreamAsync($"api/flaggednames/{FirstName}/{LastName}/{dob}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<PIC_FlaggedNames> AddFlaggedName(PIC_FlaggedNames flaggedName)
        {
            var flaggedNameJson =
                new StringContent(JsonSerializer.Serialize(flaggedName), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/flaggednames", flaggedNameJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<PIC_FlaggedNames>
                    (await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task UpdateFlaggedName(PIC_FlaggedNames flaggedName)
        {
           var flaggedNameJson = 
                new StringContent(JsonSerializer.Serialize(flaggedName),Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/flaggednames", flaggedNameJson);
        } 
        
        public async Task DeleteFlaggedName(int id)
        {
            await _httpClient.DeleteAsync($"api/flaggednames/{id}");
        }

    }
}
