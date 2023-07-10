using PicSecurityChecks.Shared;

namespace PicSecurityChecks.Client.Interfaces
{
    public interface ICurrentUserDataService
    {
        Task<string>  GetCurrentUser();
    }
}
