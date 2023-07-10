using PicSecurityChecks.Shared;

namespace PicSecurityChecks.Client.Interfaces
{
    public interface ISecurityCheckNamesDataService
    {
        Task<IEnumerable<PIC_SecurityCheckNames>> GetAllNames();
        Task<IEnumerable<PIC_SecurityCheckNames>> GetAllRecordsForUser(string userName);
        Task<PIC_SecurityCheckNames> GetRecordById(string id, string userName);
        Task UpdateRecord(string id, bool checkName);
    }
}
