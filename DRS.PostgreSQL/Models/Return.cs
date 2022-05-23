using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Return
    {
        public Return()
        {
            Commentsreturns = new HashSet<Commentsreturn>();
            Returnoperations = new HashSet<Returnoperation>();
            Returnsdetails = new HashSet<Returnsdetail>();
            Stockconfirms = new HashSet<Stockconfirm>();
        }

        public long Returnsid { get; set; }
        public long Clientid { get; set; }
        public string Returnssendedfor { get; set; }
        public int Reasonsendid { get; set; }
        public bool Returnsquoterepair { get; set; }
        public string Returnsdescription { get; set; }
        public string Returnsguidenumber { get; set; }
        public string Returnsmessagername { get; set; }
        public string Returnscc { get; set; }
        public int Returnstatusid { get; set; }
        public DateTime Returnscreatedate { get; set; }
        public long Usersid { get; set; }
        public long Employeeid { get; set; }

        public virtual Client Client { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Reasonsend Reasonsend { get; set; }
        public virtual Returnstatus Returnstatus { get; set; }
        public virtual User Users { get; set; }
        public virtual ICollection<Commentsreturn> Commentsreturns { get; set; }
        public virtual ICollection<Returnoperation> Returnoperations { get; set; }
        public virtual ICollection<Returnsdetail> Returnsdetails { get; set; }
        public virtual ICollection<Stockconfirm> Stockconfirms { get; set; }
    }
}
