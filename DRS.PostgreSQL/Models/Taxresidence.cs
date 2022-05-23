using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Taxresidence
    {
        public Taxresidence()
        {
            Raprofiles = new HashSet<Raprofile>();
        }

        public int Taxresidenceid { get; set; }
        public string Taxresidencename { get; set; }

        public virtual ICollection<Raprofile> Raprofiles { get; set; }
    }
}
