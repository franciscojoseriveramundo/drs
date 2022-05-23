using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Returnstatus
    {
        public Returnstatus()
        {
            Returns = new HashSet<Return>();
        }

        public int Returnstatusid { get; set; }
        public string Returnstatusname { get; set; }

        public virtual ICollection<Return> Returns { get; set; }
    }
}
