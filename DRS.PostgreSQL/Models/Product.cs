using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Product
    {
        public Product()
        {
            Productclients = new HashSet<Productclient>();
        }

        public int Productid { get; set; }
        public string Productname { get; set; }
        public string Productcode { get; set; }
        public string Producttechnology { get; set; }

        public virtual ICollection<Productclient> Productclients { get; set; }
    }
}
