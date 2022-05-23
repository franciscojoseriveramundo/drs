using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Statusdetail
    {
        public Statusdetail()
        {
            ReturnsdetailReturnsdetailsantennaNavigations = new HashSet<Returnsdetail>();
            ReturnsdetailReturnsdetailsbatteryNavigations = new HashSet<Returnsdetail>();
            ReturnsdetailReturnsdetailsboxNavigations = new HashSet<Returnsdetail>();
            ReturnsdetailReturnsdetailscaseNavigations = new HashSet<Returnsdetail>();
            ReturnsdetailReturnsdetailschargedNavigations = new HashSet<Returnsdetail>();
            ReturnsdetailReturnsdetailschargedbaseNavigations = new HashSet<Returnsdetail>();
            ReturnsdetailReturnsdetailsclipNavigations = new HashSet<Returnsdetail>();
            ReturnsdetailReturnsdetailscoverNavigations = new HashSet<Returnsdetail>();
            ReturnsdetailReturnsdetailsdisplayNavigations = new HashSet<Returnsdetail>();
            ReturnsdetailReturnsdetailsextractiontoolNavigations = new HashSet<Returnsdetail>();
            ReturnsdetailReturnsdetailskeyboardNavigations = new HashSet<Returnsdetail>();
            ReturnsdetailReturnsdetailsmanualNavigations = new HashSet<Returnsdetail>();
            ReturnsdetailReturnsdetailsusbNavigations = new HashSet<Returnsdetail>();
            ReturnsdetailReturnsdetailsusbcableNavigations = new HashSet<Returnsdetail>();
            ReturnsdetailReturnsdetailsusbconectorNavigations = new HashSet<Returnsdetail>();
            ReturnsdetailReturnsdetailsusbmagneticcableNavigations = new HashSet<Returnsdetail>();
        }

        public int Statusdetailsid { get; set; }
        public string Statusdetailsname { get; set; }

        public virtual ICollection<Returnsdetail> ReturnsdetailReturnsdetailsantennaNavigations { get; set; }
        public virtual ICollection<Returnsdetail> ReturnsdetailReturnsdetailsbatteryNavigations { get; set; }
        public virtual ICollection<Returnsdetail> ReturnsdetailReturnsdetailsboxNavigations { get; set; }
        public virtual ICollection<Returnsdetail> ReturnsdetailReturnsdetailscaseNavigations { get; set; }
        public virtual ICollection<Returnsdetail> ReturnsdetailReturnsdetailschargedNavigations { get; set; }
        public virtual ICollection<Returnsdetail> ReturnsdetailReturnsdetailschargedbaseNavigations { get; set; }
        public virtual ICollection<Returnsdetail> ReturnsdetailReturnsdetailsclipNavigations { get; set; }
        public virtual ICollection<Returnsdetail> ReturnsdetailReturnsdetailscoverNavigations { get; set; }
        public virtual ICollection<Returnsdetail> ReturnsdetailReturnsdetailsdisplayNavigations { get; set; }
        public virtual ICollection<Returnsdetail> ReturnsdetailReturnsdetailsextractiontoolNavigations { get; set; }
        public virtual ICollection<Returnsdetail> ReturnsdetailReturnsdetailskeyboardNavigations { get; set; }
        public virtual ICollection<Returnsdetail> ReturnsdetailReturnsdetailsmanualNavigations { get; set; }
        public virtual ICollection<Returnsdetail> ReturnsdetailReturnsdetailsusbNavigations { get; set; }
        public virtual ICollection<Returnsdetail> ReturnsdetailReturnsdetailsusbcableNavigations { get; set; }
        public virtual ICollection<Returnsdetail> ReturnsdetailReturnsdetailsusbconectorNavigations { get; set; }
        public virtual ICollection<Returnsdetail> ReturnsdetailReturnsdetailsusbmagneticcableNavigations { get; set; }
    }
}
