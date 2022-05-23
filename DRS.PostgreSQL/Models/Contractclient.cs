using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Contractclient
    {
        public long Clientid { get; set; }
        public long Contractid { get; set; }

        public virtual Contract Contract { get; set; }
    }
}
