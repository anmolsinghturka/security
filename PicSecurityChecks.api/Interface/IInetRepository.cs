using PicSecurityChecks.Shared;

namespace PicSecurityChecks.api.Interface
{
    public interface IInetRepository
    {
        IEnumerable<Inet> GetInetReturns(string firstName, string lastName);
    }
}
