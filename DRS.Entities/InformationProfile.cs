using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.Entities
{
    public class InformationProfile
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public long ClientName { get; set; }
        public string Address { get; set; }

    }
}
