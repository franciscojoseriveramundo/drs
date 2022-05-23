using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Defaultserviceorder
    {
        public Defaultserviceorder()
        {
            Raprofiles = new HashSet<Raprofile>();
        }

        public int Defaultserviceorderid { get; set; }
        public string Defaultserviceordername { get; set; }

        public virtual ICollection<Raprofile> Raprofiles { get; set; }
    }
}
