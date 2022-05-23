using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.Entities
{
    public class RecoveryRequest
    {
        public long Id { get; set; }
        public string Key { get; set; }
        public int Status { get; set; }
        public DateTime Created { get; set; }
        public long UserId { get; set; }
    }
}
