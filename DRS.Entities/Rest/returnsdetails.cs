using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.Entities.Rest
{
    public class returnsdetails
    {
        //Legacy Format

        public string name { get; set; }
        public string legend { get; set; }
        public string namefolder { get; set; }
        public string callingidenfier { get; set; }
        public bool gps { get; set; }
        public bool privatecall { get; set; }
        public string groups { get; set; }
        public string suscribesites { get; set; }

        //Multiuse Format
        public string observations { get; set; }
        public string serie { get; set; }

        //Teamvox Format
        public bool isservitron { get; set; }
        public string brandname { get; set; }
        public string alias { get; set; }
        public string group { get; set; }
        public string dataplan { get; set; }
        public string carrier { get; set; }
        public bool simisservitron { get; set; }
        public string sim { get; set; }
        public string dn { get; set; }
        public bool teamvoxlite { get; set; }
        public bool teamvoxsecuremode { get; set; }
        public bool evidencelite { get; set; }
        public bool evidenceforms { get; set; }
        public bool zaypher { get; set; }
        public bool teamvoxdispatch { get; set; }
    }
}
