using PicSecurityChecks.Shared;
using System.Security.Principal;

namespace PicSecurityChecks.api.Models
{
    public class SecurityCheckNamesRepository : ISecurityCheckNamesRepository
    {
        private readonly AppDbContext _appDbContext;

        public SecurityCheckNamesRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<PIC_SecurityCheckNames> GetAllRecords()
        {
            return _appDbContext.SecurityCheckNames;
        }

        public string GetCurrent()
        {
            string name = WindowsIdentity.GetCurrent().Name.ToString();
            return name;
        }

        public PIC_SecurityCheckNames GetRecordById(string id, string userName)
        {
            return _appDbContext.SecurityCheckNames.FirstOrDefault(e => e.applicationId == id);
        }

        public IEnumerable<PIC_SecurityCheckNames> GetRecordsForUser(string userName)
        {
            var un = WindowsIdentity.GetCurrent().Name;
            return _appDbContext.SecurityCheckNames.Where(e => e.email.Contains(userName));
        }

        public void UpdateSerurityCheck(string id, bool checkName)
        {
            var securityCheck = _appDbContext.SecurityCheckNames.FirstOrDefault(e => e.applicationId == id);
            if (securityCheck != null)
            {
                securityCheck.CheckName = checkName;
            }

            _appDbContext.SaveChanges();
        }
    }
}
