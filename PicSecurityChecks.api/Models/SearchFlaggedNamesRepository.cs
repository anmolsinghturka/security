using Microsoft.OpenApi.Services;
using PicSecurityChecks.Shared;
using System.Linq;

namespace PicSecurityChecks.api.Models
{
    public class SearchFlaggedNamesRepository : ISearchFlaggedNamesRepository
    {
        private readonly AppDbContext _appDbContext;

        public SearchFlaggedNamesRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<PIC_FlaggedNames> SearchFlaggedNames(PIC_FlaggedNames pic_FlaggedName)
        {
            if (pic_FlaggedName.firstName == "?") pic_FlaggedName.firstName = string.Empty;
            if (pic_FlaggedName.lastName == "?") pic_FlaggedName.lastName = string.Empty;
            if (pic_FlaggedName.dob == Convert.ToDateTime("1900-01-01") || pic_FlaggedName.dob is null)
            {
                if (pic_FlaggedName.lastName.Count() == 1)
                {
                    return _appDbContext.FlaggedNames.Where(c => c.firstName.ToLower().Contains(pic_FlaggedName.firstName.ToLower()))
                                                        .Where(c => c.lastName.ToLower().StartsWith(pic_FlaggedName.lastName.ToLower()))
                                                        .Where(c => c.otherNames.ToLower().Contains(pic_FlaggedName.otherNames.ToLower()))
                                                        .Where(c => c.reason.ToLower().Contains(pic_FlaggedName.reason.ToLower()))
                                                        .Where(c => c.comment.ToLower().Contains(pic_FlaggedName.comment.ToLower()))
                                                        .ToList();
                }
                else
                    return _appDbContext.FlaggedNames.Where(c => c.firstName.ToLower().Contains(pic_FlaggedName.firstName.ToLower()))
                                                        .Where(c => c.lastName.ToLower().Contains(pic_FlaggedName.lastName.ToLower()))
                                                        .Where(c => c.otherNames.ToLower().Contains(pic_FlaggedName.otherNames.ToLower()))
                                                        .Where(c => c.reason.ToLower().Contains(pic_FlaggedName.reason.ToLower()))
                                                        .Where(c => c.comment.ToLower().Contains(pic_FlaggedName.comment.ToLower()))
                                                        .ToList();
            }
            else
            {
                if (pic_FlaggedName.firstName.Length > 0 && pic_FlaggedName.lastName.Length > 0 && pic_FlaggedName.otherNames is null)
                {
                    return _appDbContext.FlaggedNames.Where(c => c.firstName.ToLower().Contains(pic_FlaggedName.firstName.ToLower()))
                                                     .Where(c => c.lastName.ToLower().Contains(pic_FlaggedName.lastName.ToLower()))
                                                     .Where(c => c.dob.Equals(pic_FlaggedName.dob));
                }
                    if (pic_FlaggedName.lastName.Count() == 1)
                {
                    return _appDbContext.FlaggedNames.Where(c => c.firstName.ToLower().Contains(pic_FlaggedName.firstName.ToLower()))
                                                    .Where(c => c.lastName.ToLower().StartsWith(pic_FlaggedName.lastName.ToLower()))
                                                    .Where(c => c.otherNames.ToLower().Contains(pic_FlaggedName.otherNames.ToLower()))
                                                    .Where(c => c.reason.ToLower().Contains(pic_FlaggedName.reason.ToLower()))
                                                    .Where(c => c.dob.Equals(pic_FlaggedName.dob))
                                                    .Where(c => c.comment.ToLower().Contains(pic_FlaggedName.comment.ToLower()))
                                                    .ToList();
                }
                else
                    return _appDbContext.FlaggedNames.Where(c => c.firstName.ToLower().Contains(pic_FlaggedName.firstName.ToLower()))
                                                    .Where(c => c.lastName.ToLower().Contains(pic_FlaggedName.lastName.ToLower()))
                                                    .Where(c => c.otherNames.ToLower().Contains(pic_FlaggedName.otherNames.ToLower()))
                                                    .Where(c => c.reason.ToLower().Contains(pic_FlaggedName.reason.ToLower()))
                                                    .Where(c => c.dob.Equals(pic_FlaggedName.dob))
                                                    .Where(c => c.comment.ToLower().Contains(pic_FlaggedName.comment.ToLower()))
                                                    .ToList();

            }

        }
    }
}
