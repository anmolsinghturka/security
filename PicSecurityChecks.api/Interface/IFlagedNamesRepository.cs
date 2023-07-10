using PicSecurityChecks.Shared;

namespace PicSecurityChecks.api.Models
{
    public interface IFlagedNamesRepository
    {
        IEnumerable<PIC_FlaggedNames> GetPIC_FlaggedNames();
        PIC_FlaggedNames GetFlaggedNameById(int id);
        int CheckFlaggedNames(string FirstName,string LastName, DateTime dob);
        PIC_FlaggedNames AddFlaggedName(PIC_FlaggedNames flaggedName);
        PIC_FlaggedNames UpdateFlaggedName(PIC_FlaggedNames flaggedName);
        void DeleteFlaggedNameById(int id);
    }
}
