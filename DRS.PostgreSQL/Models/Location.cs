using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Location
    {
        public Location()
        {
            RaprofileLocationidavailableNavigations = new HashSet<Raprofile>();
            RaprofileLocationidqualityNavigations = new HashSet<Raprofile>();
            RaprofileLocationidrepairNavigations = new HashSet<Raprofile>();
            RaprofileLocationidresidenceNavigations = new HashSet<Raprofile>();
        }

        public int Locationid { get; set; }
        public string Locationname { get; set; }
        public string Locationtype { get; set; }

        public virtual ICollection<Raprofile> RaprofileLocationidavailableNavigations { get; set; }
        public virtual ICollection<Raprofile> RaprofileLocationidqualityNavigations { get; set; }
        public virtual ICollection<Raprofile> RaprofileLocationidrepairNavigations { get; set; }
        public virtual ICollection<Raprofile> RaprofileLocationidresidenceNavigations { get; set; }
    }
}
