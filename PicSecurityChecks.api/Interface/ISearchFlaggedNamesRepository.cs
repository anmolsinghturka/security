using PicSecurityChecks.Shared;

namespace PicSecurityChecks.api.Models
{
    public interface ISearchFlaggedNamesRepository
    {
        IEnumerable<PIC_FlaggedNames> SearchFlaggedNames(PIC_FlaggedNames pic_FlaggedName);
    }
}
