using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Contract
    {
        public Contract()
        {
            Returnscontracts = new HashSet<Returnscontract>();
        }

        public long Contractid { get; set; }
        public string Contractname { get; set; }
        public string Contractcode { get; set; }

        public virtual ICollection<Returnscontract> Returnscontracts { get; set; }
    }
}
