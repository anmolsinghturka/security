using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PicSecurityChecks.Shared
{
    public class SentryResultItem
    {   
       public string subSytem { get; set; }
       public string fileNo { get; set; }   
        public string name { get; set; }
        public string gender { get; set; }
        public string dob { get; set; }
        public string Involvement { get; set; }
        public List<NicheCharge> charges { get; set; } = new List<NicheCharge>();
        public string occurType { get; set; }
        public string fps { get; set; }
        public string link { get; set; }
    }
}
