using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Distributionchannel
    {
        public Distributionchannel()
        {
            Raprofiles = new HashSet<Raprofile>();
        }

        public int Distributionchannelid { get; set; }
        public string Distributionchannelname { get; set; }

        public virtual ICollection<Raprofile> Raprofiles { get; set; }
    }
}
