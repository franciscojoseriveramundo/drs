using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Emaillog
    {
        public Emaillog()
        {
            Emaillogdestinataries = new HashSet<Emaillogdestinatary>();
        }

        public long Emaillogid { get; set; }
        public int Emaillayoutid { get; set; }
        public string Emailloghtml { get; set; }
        public DateTime Emaillogcreatedate { get; set; }
        public int Emaillogstatusid { get; set; }
        public string Emaillogsubject { get; set; }
        public DateTime? Emaillogsenddate { get; set; }
        public long Usersid { get; set; }

        public virtual Emaillayout Emaillayout { get; set; }
        public virtual User Users { get; set; }
        public virtual ICollection<Emaillogdestinatary> Emaillogdestinataries { get; set; }
    }
}
