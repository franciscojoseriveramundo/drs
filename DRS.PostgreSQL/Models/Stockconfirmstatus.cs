using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Stockconfirmstatus
    {
        public Stockconfirmstatus()
        {
            Stockconfirms = new HashSet<Stockconfirm>();
        }

        public int Stockconfirmstatusid { get; set; }
        public string Stockconfirmstatusname { get; set; }

        public virtual ICollection<Stockconfirm> Stockconfirms { get; set; }
    }
}
