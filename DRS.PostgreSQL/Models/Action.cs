using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Action
    {
        public Action()
        {
            Actionsmenus = new HashSet<Actionsmenu>();
            Permissions = new HashSet<Permission>();
        }

        public int Actionsid { get; set; }
        public string Actionsdescription { get; set; }
        public int Actionsparentid { get; set; }
        public int Actionsorderid { get; set; }
        public int Actionstypeid { get; set; }

        public virtual ICollection<Actionsmenu> Actionsmenus { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
