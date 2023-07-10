using PicSecurityChecks.Shared;

namespace PicSecurityChecks.Client.Interfaces
{
    public interface IInetDataService
    {
        Task<IEnumerable<Inet>> GetInet(string firstName, string lastName);
    }
}
