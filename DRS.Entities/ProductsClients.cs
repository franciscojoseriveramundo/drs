using System;

namespace DRS.Entities
{
    public class ProductsClients
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Serie { get; set; }
        public string Imsi { get; set; }
        public string Sim { get; set; }
        public string Number { get; set; }
        public long ClientId { get; set; }
        public int ProductId { get; set; }
        public string PlanDataId { get; set; }
        public string PlanDataName { get; set; }
        public string CarrierId { get; set; }
        public string CarrierName { get; set; }
        public string ConsoleActive { get; set; }

    }
}
