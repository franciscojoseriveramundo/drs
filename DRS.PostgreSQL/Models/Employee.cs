using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Employeeclients = new HashSet<Employeeclient>();
            Personemployees = new HashSet<Personemployee>();
            Returns = new HashSet<Return>();
        }

        public long Employeeid { get; set; }
        public string CbusinessPartnerId { get; set; }
        public string CeeUuid { get; set; }
        public string TeeId { get; set; }
        public string TypeEmployee { get; set; }

        public virtual ICollection<Employeeclient> Employeeclients { get; set; }
        public virtual ICollection<Personemployee> Personemployees { get; set; }
        public virtual ICollection<Return> Returns { get; set; }
    }
}
