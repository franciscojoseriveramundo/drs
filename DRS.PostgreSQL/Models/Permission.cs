using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Permission
    {
        public int Userroleid { get; set; }
        public int Actionsid { get; set; }

        public virtual Action Actions { get; set; }
        public virtual Userrole Userrole { get; set; }
    }
}
