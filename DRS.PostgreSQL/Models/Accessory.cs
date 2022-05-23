using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Accessory
    {
        public int Accessoriesid { get; set; }
        public string Accessoriesname { get; set; }
        public string Accessoriesnamecomplete { get; set; }
        public string Accessoriescode { get; set; }
        public string Accessoriesunit { get; set; }
        public int Accessoriesstatus { get; set; }
    }
}
