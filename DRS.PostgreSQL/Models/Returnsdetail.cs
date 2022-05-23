using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Returnsdetail
    {
        public Returnsdetail()
        {
            Stockconfirmations = new HashSet<Stockconfirmation>();
            Stockconfirmdetails = new HashSet<Stockconfirmdetail>();
        }

        public long Returnsdetailsid { get; set; }
        public bool Returnsisservitron { get; set; }
        public bool Returnsisonlyaccesory { get; set; }
        public long? Productid { get; set; }
        public string Productname { get; set; }
        public int Productquantity { get; set; }
        public string Productserialnumber { get; set; }
        public string Returnssim { get; set; }
        public int Returnsdetailsantenna { get; set; }
        public int Returnsdetailscase { get; set; }
        public int Returnsdetailsdisplay { get; set; }
        public int Returnsdetailsusb { get; set; }
        public string Returnsdetailsother { get; set; }
        public int Returnsdetailscover { get; set; }
        public int Returnsdetailsbattery { get; set; }
        public int Returnsdetailscharged { get; set; }
        public int Returnsdetailsusbcable { get; set; }
        public int Returnsdetailsusbmagneticcable { get; set; }
        public int Returnsdetailschargedbase { get; set; }
        public int Returnsdetailsclip { get; set; }
        public int Returnsdetailsmanual { get; set; }
        public int Returnsdetailsbox { get; set; }
        public int Returnsdetailsextractiontool { get; set; }
        public long Returnsid { get; set; }
        public string Returnsdetailsalias { get; set; }
        public string Returnsdetailsgroup { get; set; }
        public int? Returnsdetailsplanid { get; set; }
        public int? Returnsdetailscarrierid { get; set; }
        public string Returnsdetailslegend { get; set; }
        public string Returnsdetailsfoldername { get; set; }
        public string Returnsdetailsgroups { get; set; }
        public string Returnsdetailssuscribesites { get; set; }
        public string Returnsdetailsobservations { get; set; }
        public bool Returnsdetailssimisservitron { get; set; }
        public bool Returnsdetailsteamvoxliteopen { get; set; }
        public bool Returnsdetailsteamvoxsecuremode { get; set; }
        public bool Returnsdetailsevidencelite { get; set; }
        public bool Returnsdetailsevidenceforms { get; set; }
        public bool Returnsdetailszaypher { get; set; }
        public bool Returnsdetailsteamvoxdispatch { get; set; }
        public bool Returnsdetailscalledidentifier { get; set; }
        public bool Returnsdetailsgps { get; set; }
        public bool Returnsdetailscallprivate { get; set; }
        public string Returnsdetailsdialnumber { get; set; }
        public int Returnsdetailskeyboard { get; set; }
        public int Returnsdetailsusbconector { get; set; }

        public virtual Return Returns { get; set; }
        public virtual Statusdetail ReturnsdetailsantennaNavigation { get; set; }
        public virtual Statusdetail ReturnsdetailsbatteryNavigation { get; set; }
        public virtual Statusdetail ReturnsdetailsboxNavigation { get; set; }
        public virtual Statusdetail ReturnsdetailscaseNavigation { get; set; }
        public virtual Statusdetail ReturnsdetailschargedNavigation { get; set; }
        public virtual Statusdetail ReturnsdetailschargedbaseNavigation { get; set; }
        public virtual Statusdetail ReturnsdetailsclipNavigation { get; set; }
        public virtual Statusdetail ReturnsdetailscoverNavigation { get; set; }
        public virtual Statusdetail ReturnsdetailsdisplayNavigation { get; set; }
        public virtual Statusdetail ReturnsdetailsextractiontoolNavigation { get; set; }
        public virtual Statusdetail ReturnsdetailskeyboardNavigation { get; set; }
        public virtual Statusdetail ReturnsdetailsmanualNavigation { get; set; }
        public virtual Statusdetail ReturnsdetailsusbNavigation { get; set; }
        public virtual Statusdetail ReturnsdetailsusbcableNavigation { get; set; }
        public virtual Statusdetail ReturnsdetailsusbconectorNavigation { get; set; }
        public virtual Statusdetail ReturnsdetailsusbmagneticcableNavigation { get; set; }
        public virtual ICollection<Stockconfirmation> Stockconfirmations { get; set; }
        public virtual ICollection<Stockconfirmdetail> Stockconfirmdetails { get; set; }
    }
}
