using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Clientaddress
    {
        public long Clientaddressid { get; set; }
        public long Clientid { get; set; }
        public string Clientaddresscityname { get; set; }
        public string Clientaddressdistrictname { get; set; }
        public string Clientaddressemail { get; set; }
        public string Clientaddressnumber { get; set; }
        public string Clientaddressphonenumber { get; set; }
        public string Clientaddressstreetname { get; set; }
        public string Clientaddressregioncode { get; set; }
        public string Clientaddresscountry { get; set; }
        public string Clientaddresslegalname { get; set; }
        public string Clientaddresspostalcode { get; set; }

        public virtual Client Client { get; set; }
    }
}
