using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.Entities
{
    public class RAprofile
    {
        public long Raprofileid { get; set; }
        public long Personid { get; set; }
        public int Unitsalesid { get; set; }
        public int Taxresidenceid { get; set; }
        public int Locationidresidence { get; set; }
        public int Locationidrepair { get; set; }
        public int Locationidavailable { get; set; }
        public int Locationidquality { get; set; }
        public int Distributionchannelid { get; set; }
        public int Defaultserviceorderid { get; set; }
    }
}
