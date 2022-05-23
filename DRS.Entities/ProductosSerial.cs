using System;
using System.Collections.Generic;
using System.Text;

namespace DRS.Entities
{
    public class ProductosSerial
    {
        public string Serial { get; set; }
        public string SerialDescription { get; set; }
        public long ProductId { get; set; }
        public long ClientId { get; set; }
        public string SIM { get; set; }
        public string Dn { get; set; }
    }
}
