using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.Entities
{
    public class Menu
    {
        public int Menuid { get; set; }
        public string Menuname { get; set; }
        public string Menucontroller { get; set; }
        public string Menuaction { get; set; }
        public string Menuicon { get; set; }
        public int Menuparentid { get; set; }
        public int Menuislinked { get; set; }
        public int Menuenabled { get; set; }
        public int Menuorder { get; set; }
        public DateTime Menucreatedate { get; set; }
    }
}
