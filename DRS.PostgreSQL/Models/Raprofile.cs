using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Raprofile
    {
        public long Raprofileid { get; set; }
        public long Personid { get; set; }
        public int Unitsalesid { get; set; }
        public int Taxresidenceid { get; set; }
        public int Locationidresidence { get; set; }
        public int Locationidrepair { get; set; }
        public int Locationidavailable { get; set; }
        public int Locationidquality { get; set; }
        public int Distributionchannelid { get; set; }
        public int Defaultserviceorderid { get; set; }

        public virtual Defaultserviceorder Defaultserviceorder { get; set; }
        public virtual Distributionchannel Distributionchannel { get; set; }
        public virtual Location LocationidavailableNavigation { get; set; }
        public virtual Location LocationidqualityNavigation { get; set; }
        public virtual Location LocationidrepairNavigation { get; set; }
        public virtual Location LocationidresidenceNavigation { get; set; }
        public virtual Person Person { get; set; }
        public virtual Taxresidence Taxresidence { get; set; }
        public virtual Unitsale Unitsales { get; set; }
    }
}
