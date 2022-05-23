using System;
using System.Collections.Generic;
using System.Text;

namespace DRS.Entities
{
    public class Productos
    {
        public int ProductoId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public long ClientId { get; set; }
        public string Sim { get; set; }
        public string ProductTechnology { get; set; }
        public int CarrierId { get; set; }
        public int PlanId { get; set; }
        public string Serie { get; set; }
    }
}
