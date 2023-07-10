using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSecurityChecks.Shared
{
    public class SearchParam
    {
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public DateTime DOB { get; set; }
        public string? Gender { get; set; }
    }
}
