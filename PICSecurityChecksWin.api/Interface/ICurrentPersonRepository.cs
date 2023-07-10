using PicSecurityChecks.Shared;

namespace PicSecurityChecksWin.api.Interface
{
    public interface ICurrentPersonRepository
    {
        CurrentPerson GetCurrentPerson();
    }
}
