﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.Entities
{
    public class PersonEmployee
    {
        public long EmployeeId { get; set; }
        public string CBusinessPartnerId { get; set; }
        public string CeeUui { get; set; }
        public string TeeUi { get; set; }
        public string TypeEmployee { get; set; }
    }
}
