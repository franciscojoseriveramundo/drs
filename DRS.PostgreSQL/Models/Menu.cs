using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Menu
    {
        public Menu()
        {
            Actionsmenus = new HashSet<Actionsmenu>();
            Menuuserroles = new HashSet<Menuuserrole>();
        }

        public int Menuid { get; set; }
        public string Menuname { get; set; }
        public string Menucontroller { get; set; }
        public string Menuaction { get; set; }
        public string Menuicon { get; set; }
        public int Menuparentid { get; set; }
        public int Menuislinked { get; set; }
        public int Menuenabled { get; set; }
        public int Menuorder { get; set; }
        public DateTime Menucreatedate { get; set; }

        public virtual ICollection<Actionsmenu> Actionsmenus { get; set; }
        public virtual ICollection<Menuuserrole> Menuuserroles { get; set; }
    }
}
