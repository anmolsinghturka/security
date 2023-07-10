using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSecurityChecks.Shared
{
    public class NicheResult
    {
        public int id { get; set; }
        public string name { get; set; }
        public int resultHits { get; set; }
        public List<SentryResultItem> results { get; set; } = new List<SentryResultItem>();
    }
}
