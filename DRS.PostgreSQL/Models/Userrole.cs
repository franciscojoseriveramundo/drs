using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Userrole
    {
        public Userrole()
        {
            Menuuserroles = new HashSet<Menuuserrole>();
            Permissions = new HashSet<Permission>();
            Users = new HashSet<User>();
        }

        public int Userroleid { get; set; }
        public string Userrolename { get; set; }

        public virtual ICollection<Menuuserrole> Menuuserroles { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
