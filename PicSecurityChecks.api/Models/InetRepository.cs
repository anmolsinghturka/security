using Microsoft.EntityFrameworkCore;
using PicSecurityChecks.api.Interface;
using PicSecurityChecks.Shared;
using System.Linq.Expressions;

namespace PicSecurityChecks.api.Models
{
    public class InetRepository : IInetRepository
    {
        private readonly AppDbContext _appDbContext;

        public InetRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Inet> GetInetReturns(string firstName, string lastName)
        {
            
            return _appDbContext.inets.FromSql($"UNIQ_getSQLINETListByName {firstName} {lastName}");
            
        }
    }
}
