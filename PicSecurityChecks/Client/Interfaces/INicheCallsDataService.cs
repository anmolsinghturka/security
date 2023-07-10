using Microsoft.AspNetCore.Components;
using PicSecurityChecks.Shared;

namespace PicSecurityChecks.Client.Interfaces
{
    public interface INicheCallsDataService
    {
        Task<NicheReturnedData> GetNicheQuery(string lastName, string firstName);
        Task<NicheReturnedData> GetOtherNiche(string path);

    }
}
    