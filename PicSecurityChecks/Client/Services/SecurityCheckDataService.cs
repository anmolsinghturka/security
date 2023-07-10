using Microsoft.AspNetCore.Components;
using PicSecurityChecks.Client.Interfaces;
using PicSecurityChecks.Shared;
using System.Text.Json;

namespace PicSecurityChecks.Client.Services
{
    public class SecurityCheckDataService : ISecurityCheckNamesDataService
    {
        private readonly HttpClient _httpClient;

        public SecurityCheckDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<PIC_SecurityCheckNames>> GetAllNames()
        {
            IEnumerable<PIC_SecurityCheckNames> records;

            records = await JsonSerializer.DeserializeAsync<IEnumerable<PIC_SecurityCheckNames>>
                (await _httpClient.GetStreamAsync($"api/securitychecknames/"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            if (records == null)
            {
                records = new List<PIC_SecurityCheckNames>();
            }
            return records;

        }

        public async Task<IEnumerable<PIC_SecurityCheckNames>> GetAllRecordsForUser(string userName)
        {
            IEnumerable<PIC_SecurityCheckNames> records;
            records = await JsonSerializer.DeserializeAsync<IEnumerable<PIC_SecurityCheckNames>>
                (await _httpClient.GetStreamAsync($"api/securitychecknames/{userName}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            if (records == null )
            {
                records = new List<PIC_SecurityCheckNames>();
            }
            return records;
        }

        public async Task<PIC_SecurityCheckNames> GetRecordById(string id, string userName)
        {

            PIC_SecurityCheckNames pIC_SecurityCheckNames = null;

            pIC_SecurityCheckNames= await JsonSerializer.DeserializeAsync<PIC_SecurityCheckNames>
                (await _httpClient.GetStreamAsync($"api/securitychecknames/{id}/{userName}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            if (pIC_SecurityCheckNames == null )
            {
                pIC_SecurityCheckNames = new PIC_SecurityCheckNames();
            }
            return pIC_SecurityCheckNames;
        }

        public async Task UpdateRecord(string id, bool checkName)
        {
            await _httpClient.DeleteAsync($"api/securitychecknames/{id}/{checkName}");
        }
    }
}
