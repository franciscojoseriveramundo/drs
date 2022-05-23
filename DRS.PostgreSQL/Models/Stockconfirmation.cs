using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Stockconfirmation
    {
        public Stockconfirmation()
        {
            Stockconfirmationdetails = new HashSet<Stockconfirmationdetail>();
            Stockconfirmdetails = new HashSet<Stockconfirmdetail>();
        }

        public long Stockconfirmationid { get; set; }
        public long Returnsdetailsid { get; set; }
        public int Stockconfirmationstatusid { get; set; }

        public virtual Returnsdetail Returnsdetails { get; set; }
        public virtual Stockconfirmationstatus Stockconfirmationstatus { get; set; }
        public virtual ICollection<Stockconfirmationdetail> Stockconfirmationdetails { get; set; }
        public virtual ICollection<Stockconfirmdetail> Stockconfirmdetails { get; set; }
    }
}
