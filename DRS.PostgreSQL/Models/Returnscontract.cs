using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Returnscontract
    {
        public long Returnsid { get; set; }
        public long Contractid { get; set; }

        public virtual Contract Contract { get; set; }
    }
}
