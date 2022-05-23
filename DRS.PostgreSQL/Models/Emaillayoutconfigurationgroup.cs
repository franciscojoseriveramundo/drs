using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Emaillayoutconfigurationgroup
    {
        public Emaillayoutconfigurationgroup()
        {
            Emaildestinataryconfigurationgroups = new HashSet<Emaildestinataryconfigurationgroup>();
            Emaillayoutconfigurations = new HashSet<Emaillayoutconfiguration>();
        }

        public int Emaillayoutconfigurationgroupid { get; set; }
        public string Emaillayoutconfigurationgroupname { get; set; }

        public virtual ICollection<Emaildestinataryconfigurationgroup> Emaildestinataryconfigurationgroups { get; set; }
        public virtual ICollection<Emaillayoutconfiguration> Emaillayoutconfigurations { get; set; }
    }
}
