using System;
using System.Collections.Generic;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class Commentsreturn
    {
        public long Commentsreturnsid { get; set; }
        public long Usersid { get; set; }
        public string Commentsreturnsdescription { get; set; }
        public DateTime Commentsreturnscreatedate { get; set; }
        public long Returnsid { get; set; }

        public virtual Return Returns { get; set; }
        public virtual User Users { get; set; }
    }
}
