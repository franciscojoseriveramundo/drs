using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Stockconfirm
    {
        public Stockconfirm()
        {
            Stockconfirmdetails = new HashSet<Stockconfirmdetail>();
        }

        public long Stockconfirmid { get; set; }
        public DateTime? Stockconfirmcreatedate { get; set; }
        public int Stockconfirmstatusid { get; set; }
        public string Stockconfirmsapcode { get; set; }
        public string Stockconfirmsapuuid { get; set; }
        public long Returnid { get; set; }

        public virtual Return Return { get; set; }
        public virtual Stockconfirmstatus Stockconfirmstatus { get; set; }
        public virtual ICollection<Stockconfirmdetail> Stockconfirmdetails { get; set; }
    }
}
