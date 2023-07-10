using PicSecurityChecks.Shared;

namespace PicSecurityChecks.Client.Interfaces
{
    public interface IFlaggedNamesDataService
    {
        Task<IEnumerable<PIC_FlaggedNames>> GetPIC_FlaggedNames();
        Task<PIC_FlaggedNames> GetFlaggedNamesById(int id);
        Task<int> CheckFlaggedNames(string FirstName, string LastName, string dob); 
        Task<PIC_FlaggedNames> AddFlaggedName(PIC_FlaggedNames flaggedName);
        Task UpdateFlaggedName(PIC_FlaggedNames flaggedName);
        Task DeleteFlaggedName(int id);
    }
}
