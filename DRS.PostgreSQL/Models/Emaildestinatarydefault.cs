using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Emaildestinatarydefault
    {
        public Emaildestinatarydefault()
        {
            Emaildestinataryconfigurationgroups = new HashSet<Emaildestinataryconfigurationgroup>();
        }

        public int Emaildestinatarydefaultid { get; set; }
        public string Emaildestinatarydefaultemail { get; set; }

        public virtual ICollection<Emaildestinataryconfigurationgroup> Emaildestinataryconfigurationgroups { get; set; }
    }
}
