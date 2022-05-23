using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class User
    {
        public User()
        {
            Commentsreturns = new HashSet<Commentsreturn>();
            Emaillogs = new HashSet<Emaillog>();
            Recoveries = new HashSet<Recovery>();
            Returnoperations = new HashSet<Returnoperation>();
            Returns = new HashSet<Return>();
            Userskeys = new HashSet<Userskey>();
        }

        public long Usersid { get; set; }
        public int Userrole { get; set; }
        public int Userstatusid { get; set; }
        public long Personid { get; set; }
        public string Usersemail { get; set; }
        public string Usersphonenumber { get; set; }

        public virtual Person Person { get; set; }
        public virtual Userrole UserroleNavigation { get; set; }
        public virtual Userstatus Userstatus { get; set; }
        public virtual ICollection<Commentsreturn> Commentsreturns { get; set; }
        public virtual ICollection<Emaillog> Emaillogs { get; set; }
        public virtual ICollection<Recovery> Recoveries { get; set; }
        public virtual ICollection<Returnoperation> Returnoperations { get; set; }
        public virtual ICollection<Return> Returns { get; set; }
        public virtual ICollection<Userskey> Userskeys { get; set; }
    }
}
