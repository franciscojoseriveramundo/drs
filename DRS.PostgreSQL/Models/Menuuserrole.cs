using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Menuuserrole
    {
        public int Menuid { get; set; }
        public int Userroleid { get; set; }

        public virtual Menu Menu { get; set; }
        public virtual Userrole Userrole { get; set; }
    }
}
