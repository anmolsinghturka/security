using Microsoft.AspNetCore.Http.HttpResults;
using PicSecurityChecks.api.Interface;
using PicSecurityChecks.Shared;
using System.Net;
using System.Text;

namespace PicSecurityChecks.api.Models
{
    public class GetNicheDataRepository : IGetNicheDataRepository
    {
        private IConfiguration _configuration;

        public GetNicheDataRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<NicheReturnedData> GetData(string lastName, string firstName)
        {
            NicheReturnedData data = new NicheReturnedData();
            StringBuilder sql = new StringBuilder();
            var endpoint = _configuration["NDS:Connection"];
            //string sql = "Select top 1 id from occurrence";
            var domain = _configuration["NDS:Domain"];
            var userName = _configuration["NDS:UserName"];
            var password = _configuration["NDS:Password"];

            HttpClient client = new HttpClient();

            client = new HttpClient()
            {
                BaseAddress = new Uri(endpoint)
            };

            sql.Append("select GPerson__id as PersonId, GPersonName__id, gOccurrence.OccurrenceFileNo as CaseNo, gOccurrence.id as Id, ");
            sql.Append("   GPersonName.Gender, GPersonName.DateOfBirthG as DateOfBirth, Gperson.NationalId as FPS, GPersonName.Type1G, ");
            sql.Append("   GPersonName.Surname as Name, GPersonName.Given1 , GPersonName.Given2 , GOccIvGPerson.Classification, GOccIvGPerson.ClassificationG, ");
            sql.Append("   GPersonName.SearchScore, Person.CautionSummaryLabel, Occurrence.OccurrenceStdOccTypeRId_L as OccurrenceType ");
            sql.Append($" from GPersonName.ScoredName(Surname='{lastName.Replace("'", "''")} ', Given1='{firstName.Replace("'", "''")}')");
            sql.Append(" LEFT JOIN (GPerson LEFT JOIN Person LEFT JOIN (GOccIvGPerson left join (gOccurrence left join Occurrence)))");
            sql.Append($" where GPersonName.Surname LIKE '{lastName.Replace("'", "''")}' ");
            sql.Append($" and GPersonName.Given1 LIKE '{firstName.Replace("'", "''")}' ");
            sql.Append(" and gOccurrence.OccurrenceFileNo <> '' ");
            sql.Append(" order by GPersonName.DateOfBirthG, gOccurrence.OccurrenceFileNo");

            client.Timeout = TimeSpan.FromMinutes(60);

            client.DefaultRequestHeaders.Add("Domain", domain);
            client.DefaultRequestHeaders.Add("Username", userName);
            client.DefaultRequestHeaders.Add("Password", password);


            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            string paths = $"query/run?command={sql.ToString()}";

            var response = await client.GetAsync(paths);
            var xmlResults = await response.Content.ReadAsStringAsync();
            data.ReturnedData = xmlResults;
            return data;
        }

        public async Task<NicheReturnedData> GetDataByPath(string path)
        {
            NicheReturnedData data = new NicheReturnedData();

            var endpoint = _configuration["NDS:Connection"];
            //string sql = "Select top 1 id from occurrence";
            var domain = _configuration["NDS:Domain"];
            var userName = _configuration["NDS:UserName"];
            var password = _configuration["NDS:Password"];

            HttpClient client = new HttpClient();

            client = new HttpClient()
            {
                BaseAddress = new Uri(endpoint)
            };


            client.Timeout = TimeSpan.FromMinutes(60);

            client.DefaultRequestHeaders.Add("Domain", domain);
            client.DefaultRequestHeaders.Add("Username", userName);
            client.DefaultRequestHeaders.Add("Password", password);


            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            string paths = $"query/run?command={path}";

            var response = await client.GetAsync(paths);
            var xmlResults = await response.Content.ReadAsStringAsync();
            data.ReturnedData = xmlResults;
            return data;
        }
    }
}
