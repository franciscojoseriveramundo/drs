using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Returnoperation
    {
        public int Returnoperationsid { get; set; }
        public long Usersid { get; set; }
        public DateTime Returnoperationsdate { get; set; }
        public string Returnoperationsdescription { get; set; }
        public long Returnid { get; set; }

        public virtual Return Return { get; set; }
        public virtual User Users { get; set; }
    }
}
