using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Log
    {
        public long Logsid { get; set; }
        public string Logsdescription { get; set; }
        public long Userid { get; set; }
        public string Logsorigin { get; set; }
        public DateTime Logscreatedate { get; set; }
    }
}
