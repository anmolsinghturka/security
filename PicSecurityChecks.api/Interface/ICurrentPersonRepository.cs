using PicSecurityChecks.Shared;

namespace PicSecurityChecks.api.Interface
{
    public interface ICurrentPersonRepository
    {
        CurrentPerson GetCurrentPerson();
    }
}
