using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Reasonsend
    {
        public Reasonsend()
        {
            Returns = new HashSet<Return>();
        }

        public int Reasonsendid { get; set; }
        public string Reasonsendname { get; set; }

        public virtual ICollection<Return> Returns { get; set; }
    }
}
