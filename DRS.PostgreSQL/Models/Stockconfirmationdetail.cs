using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Stockconfirmationdetail
    {
        public Stockconfirmationdetail()
        {
            Stockconfirmdetails = new HashSet<Stockconfirmdetail>();
        }

        public long Stockconfirmationdetailsid { get; set; }
        public long Stockconfirmationid { get; set; }
        public int Stockconfirmationquantity { get; set; }
        public string Stockconfirmationcomments { get; set; }
        public long Usersid { get; set; }
        public DateTime Stockconfirmationcreatedate { get; set; }

        public virtual Stockconfirmation Stockconfirmation { get; set; }
        public virtual ICollection<Stockconfirmdetail> Stockconfirmdetails { get; set; }
    }
}
