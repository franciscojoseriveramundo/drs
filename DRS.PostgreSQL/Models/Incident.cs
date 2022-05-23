using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Incident
    {
        public long Incidentsid { get; set; }
        public long Usersid { get; set; }
        public string Incidentsdescription { get; set; }
        public DateTime Incidentscreatedate { get; set; }
    }
}
