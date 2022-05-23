using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Stockconfirmationstatus
    {
        public Stockconfirmationstatus()
        {
            Stockconfirmations = new HashSet<Stockconfirmation>();
        }

        public int Stockconfirmationstatusid { get; set; }
        public string Stockconfirmationstatusname { get; set; }

        public virtual ICollection<Stockconfirmation> Stockconfirmations { get; set; }
    }
}
