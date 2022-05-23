using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Stockconfirmdetail
    {
        public long Stockconfirmdetailsid { get; set; }
        public int Stockconfirmorderid { get; set; }
        public int Productid { get; set; }
        public long Returndetailsid { get; set; }
        public long Stockconfirmid { get; set; }
        public long Stockconfirmationid { get; set; }
        public long Stockconfirmationdetailsid { get; set; }

        public virtual Returnsdetail Returndetails { get; set; }
        public virtual Stockconfirm Stockconfirm { get; set; }
        public virtual Stockconfirmation Stockconfirmation { get; set; }
        public virtual Stockconfirmationdetail Stockconfirmationdetails { get; set; }
    }
}
