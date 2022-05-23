using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Emaillayout
    {
        public Emaillayout()
        {
            Emaillayoutconfigurations = new HashSet<Emaillayoutconfiguration>();
            Emaillogs = new HashSet<Emaillog>();
        }

        public int Emaillayoutid { get; set; }
        public string Emaillayoutname { get; set; }
        public string Emaillayoutbody { get; set; }

        public virtual ICollection<Emaillayoutconfiguration> Emaillayoutconfigurations { get; set; }
        public virtual ICollection<Emaillog> Emaillogs { get; set; }
    }
}
