using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Person
    {
        public Person()
        {
            Personemployees = new HashSet<Personemployee>();
            Raprofiles = new HashSet<Raprofile>();
            Users = new HashSet<User>();
        }

        public long Personid { get; set; }
        public string Personname { get; set; }
        public string Personlastname { get; set; }
        public string Persongender { get; set; }
        public bool Personisemployee { get; set; }

        public virtual ICollection<Personemployee> Personemployees { get; set; }
        public virtual ICollection<Raprofile> Raprofiles { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
