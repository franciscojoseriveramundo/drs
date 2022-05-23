using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Emaillayoutconfiguration
    {
        public int Emaillayoutconfigurationid { get; set; }
        public int Emaillayoutconfigurationgroupid { get; set; }
        public int Emaillayoutid { get; set; }

        public virtual Emaillayout Emaillayout { get; set; }
        public virtual Emaillayoutconfigurationgroup Emaillayoutconfigurationgroup { get; set; }
    }
}
