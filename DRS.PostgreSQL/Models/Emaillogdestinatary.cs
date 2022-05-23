using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Emaillogdestinatary
    {
        public long Emaillogdestinataryid { get; set; }
        public long Emaillogid { get; set; }
        public string Emaillogdestinataryemail { get; set; }
        public int Emaillogdestinatarytype { get; set; }

        public virtual Emaillog Emaillog { get; set; }
    }
}
