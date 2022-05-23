using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.Entities
{
    public class UsersViewModel
    {
        public Int64 UserId { get; set; }
        public string Username { get; set; }
        public string Useremail { get; set; }
        public string Userrole { get; set; }
        public string Userstatus { get; set; }
        public int Userstatusid { get; set; }
    }
}
