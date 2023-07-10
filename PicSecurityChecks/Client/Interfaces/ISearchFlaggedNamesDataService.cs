using PicSecurityChecks.Shared;

namespace PicSecurityChecks.Client.Interfaces
{
    public interface ISearchFlaggedNamesDataService
    {
        Task<IEnumerable<PIC_FlaggedNames>> GetFlaggedNamesSearch(PIC_FlaggedNames pic_FlaggedName);
    }
}
