using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.Entities
{
    public class ClientAddress
    {
        public long Clientaddressid { get; set; }
        public string Clientid { get; set; }
        public string Clientaddresscityname { get; set; }
        public string Clientaddressdistrictname { get; set; }
        public string Clientaddressemail { get; set; }
        public string Clientaddressnumber { get; set; }
        public string Clientaddressphonenumber { get; set; }
        public string Clientaddressstreetname { get; set; }
        public string Clientaddressregioncode { get; set; }
        public string Clientaddresscountry { get; set; }
        public string Clientaddressappoderatename { get; set; }
        public string Clientaddresspostalcode { get; set; }
    }
}
