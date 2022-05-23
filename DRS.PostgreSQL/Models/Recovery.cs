using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Recovery
    {
        public long Recoveryid { get; set; }
        public string Recoverykey { get; set; }
        public int Recoverystatus { get; set; }
        public long Usersid { get; set; }
        public DateTime Recoverycreatedate { get; set; }

        public virtual User Users { get; set; }
    }
}
