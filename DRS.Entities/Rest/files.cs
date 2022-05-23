using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.Entities.Rest
{
    public class files
    {
        public byte[] fileContent { get; set; }
        public string nameFile { get; set; }
        public string extensionFile { get; set; }
    }
}
