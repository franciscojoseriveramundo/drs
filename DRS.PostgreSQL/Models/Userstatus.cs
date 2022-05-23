using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Userstatus
    {
        public Userstatus()
        {
            Users = new HashSet<User>();
        }

        public int Userstatusid { get; set; }
        public string Userstatusname { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
