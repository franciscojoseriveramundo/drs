using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Productclient
    {
        public long Productclientid { get; set; }
        public int Productid { get; set; }
        public long Clientid { get; set; }
        public string Serie { get; set; }
        public string Imsi { get; set; }
        public string Number { get; set; }
        public string Sim { get; set; }
        public int? Planid { get; set; }
        public string Planname { get; set; }
        public int? Carrierid { get; set; }
        public string Carriername { get; set; }
        public string Consoleactive { get; set; }

        public virtual Client Client { get; set; }
        public virtual Product Product { get; set; }
    }
}
