using PicSecurityChecks.Shared;

namespace PicSecurityChecksWin.api.Models
{
    public interface ISecurityCheckNamesRepository
    {
        IEnumerable<PIC_SecurityCheckNames> GetAllRecords();
        IEnumerable<PIC_SecurityCheckNames> GetRecordsForUser(string userName);
        PIC_SecurityCheckNames GetRecordById(string id, string userName);
        void UpdateSerurityCheck(string id, bool checkName);
        string GetCurrent();
    }
}
