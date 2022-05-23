using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Personemployee
    {
        public long Personid { get; set; }
        public long Employeeid { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Person Person { get; set; }
    }
}
