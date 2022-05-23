using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.Entities
{
    public class Actions
    {
        public int Actionsid { get; set; }
        public string Actionsdescription { get; set; }
        public int Actionsparentid { get; set; }
        public int Actionsorderid { get; set; }
        public int Actionstypeid { get; set; }

    }
}
