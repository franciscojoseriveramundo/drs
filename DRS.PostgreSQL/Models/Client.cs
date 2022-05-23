using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Client
    {
        public Client()
        {
            Clientaddresses = new HashSet<Clientaddress>();
            Employeeclients = new HashSet<Employeeclient>();
            Productclients = new HashSet<Productclient>();
            Returns = new HashSet<Return>();
        }

        public long Clientid { get; set; }
        public string Clientname { get; set; }
        public string Clientcode { get; set; }
        public int? Clientstatus { get; set; }

        public virtual ICollection<Clientaddress> Clientaddresses { get; set; }
        public virtual ICollection<Employeeclient> Employeeclients { get; set; }
        public virtual ICollection<Productclient> Productclients { get; set; }
        public virtual ICollection<Return> Returns { get; set; }
    }
}
