using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Unitsale
    {
        public Unitsale()
        {
            Raprofiles = new HashSet<Raprofile>();
        }

        public int Unitsalesid { get; set; }
        public string Unitsalename { get; set; }

        public virtual ICollection<Raprofile> Raprofiles { get; set; }
    }
}
