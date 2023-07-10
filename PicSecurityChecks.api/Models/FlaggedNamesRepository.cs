using PicSecurityChecks.Shared;

namespace PicSecurityChecks.api.Models
{
    public class FlaggedNamesRepository : IFlagedNamesRepository
    {
        private readonly AppDbContext _appDbContext;

        public FlaggedNamesRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<PIC_FlaggedNames> GetPIC_FlaggedNames()
        {
            return _appDbContext.FlaggedNames.Take(100);
        }

        public PIC_FlaggedNames GetFlaggedNameById(int id)
        {
            return _appDbContext.FlaggedNames.FirstOrDefault(e => e.Id == id);
        }

        public int CheckFlaggedNames(string FirstName, string LastName, DateTime dob)
        {
            return _appDbContext.FlaggedNames.Where(c => c.firstName == FirstName)
                                             .Where(c => c.lastName == LastName)
                                             .Where(c => c.dob == dob)
                                             .Count();
        }

        public PIC_FlaggedNames AddFlaggedName(PIC_FlaggedNames flaggedName)
        {
           var addedFlaggedName = _appDbContext.FlaggedNames.Add(flaggedName);
            _appDbContext.SaveChanges();
            return addedFlaggedName.Entity;
        }
       public PIC_FlaggedNames UpdateFlaggedName(PIC_FlaggedNames flaggedName)
        {
            var foundFlaggedName = _appDbContext.FlaggedNames.FirstOrDefault(e => e.Id == flaggedName.Id);

            if (foundFlaggedName != null)
            {
                foundFlaggedName.firstName = flaggedName.firstName;
                foundFlaggedName.lastName = flaggedName.lastName;
                foundFlaggedName.otherNames = flaggedName.otherNames;
                foundFlaggedName.comment = flaggedName.comment;
                foundFlaggedName.modifiedAt = flaggedName.modifiedAt;
                foundFlaggedName.dob = flaggedName.dob;
                foundFlaggedName.modifiedBy = flaggedName.modifiedBy;
                foundFlaggedName.reason = flaggedName.reason;

                _appDbContext.SaveChanges();

                return foundFlaggedName;
            }

            return null;
        }

        public void DeleteFlaggedNameById(int id)
        {
            var foundFlaggedName = _appDbContext.FlaggedNames.FirstOrDefault(e =>e.Id == id);
            if (foundFlaggedName == null) return; 

            _appDbContext.FlaggedNames.Remove(foundFlaggedName);
            _appDbContext.SaveChanges();
        }

    }
}
