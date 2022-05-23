using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Emaildestinataryconfigurationgroup
    {
        public int Emaildestinataryconfigurationgroupid { get; set; }
        public int Emaillayoutconfigurationgroupid { get; set; }
        public int Emaildestinatarydefaultid { get; set; }

        public virtual Emaildestinatarydefault Emaildestinatarydefault { get; set; }
        public virtual Emaillayoutconfigurationgroup Emaillayoutconfigurationgroup { get; set; }
    }
}
