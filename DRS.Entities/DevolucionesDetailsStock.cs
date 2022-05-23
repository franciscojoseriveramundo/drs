using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.Entities
{
    public class DevolucionesDetailsStock
    {
        public long returndetailsid { get; set; }
        public string nameproduct { get; set; }
        public string quantity  { get; set; }
        public int statusid { get; set; }
        public string status { get; set; }
        public string lastquantityreported { get; set; }
        public string lastcommentreported { get; set; }
    }
}
