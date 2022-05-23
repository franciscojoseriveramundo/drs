using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Operationtype
    {
        public Operationtype()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int Operationtypeid { get; set; }
        public string Operationtypename { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
