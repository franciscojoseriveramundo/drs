using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Transaction
    {
        public long Transactionsid { get; set; }
        public string Transactionsdescription { get; set; }
        public DateTime Transactionscreatedate { get; set; }
        public int Operationtypeid { get; set; }
        public long Usersid { get; set; }

        public virtual Operationtype Operationtype { get; set; }
    }
}
