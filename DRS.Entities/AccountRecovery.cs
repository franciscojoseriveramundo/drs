using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.Entities
{
    public class AccountRecovery
    {
        public long UserId { get; set; }
        public string Key { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword   { get; set; }
    }
}
