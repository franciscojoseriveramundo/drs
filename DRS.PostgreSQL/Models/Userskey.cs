using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Userskey
    {
        public long Userskeysid { get; set; }
        public string Userskeyspassword { get; set; }
        public long Usersid { get; set; }
        public int Userskeysenabled { get; set; }
        public DateTime Userskeyscreatedate { get; set; }

        public virtual User Users { get; set; }
    }
}
