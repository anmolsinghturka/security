using PicSecurityChecks.Shared;

namespace PicSecurityChecksWin.api.Models
{
    public interface ISearchFlaggedNamesRepository
    {
        IEnumerable<PIC_FlaggedNames> SearchFlaggedNames(PIC_FlaggedNames pic_FlaggedName);
    }
}
