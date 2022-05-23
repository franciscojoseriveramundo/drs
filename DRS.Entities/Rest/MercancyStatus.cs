using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.Entities.Rest
{
    public class MercancyStatus
    {
        public long Code { get; set; }
        public string Log { get; set; }
        public List<MercancyStatusDetails> MercancyStatusDetails { get; set; }
    }
}
