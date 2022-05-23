using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.Entities.Rest
{
    public class returns
    {
        public long returnsid { get; set; }
        public string reasonsend { get; set; }
        public string legalappoderate { get; set; }
        public string ra { get; set; }
        public string email { get; set; }
        public string numbertelephone { get; set; }
        public string clientcode { get; set; }
        public string address { get; set; }
        public string appoderategalclient { get; set; }
        public string emailclient { get; set; }
        public string numbertelephoneclient { get; set; }
        public int typedocument { get; set; }

        public List<returnsdetails> returnsdetails { get; set; }
    }
}
