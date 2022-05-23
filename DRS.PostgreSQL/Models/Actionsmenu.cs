using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Actionsmenu
    {
        public int Actionsid { get; set; }
        public int Menuid { get; set; }

        public virtual Action Actions { get; set; }
        public virtual Menu Menu { get; set; }
    }
}
