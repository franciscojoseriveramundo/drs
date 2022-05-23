using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.Entities
{
    public class Users
    {
        public Int64 usersid { get; set; }
        public string personname { get; set; }
        public Int64 personid { get; set; }
        public string personlastname { get; set; }
        public string usersemail { get; set; }
        public List<PersonEmployee> cee_uuid { get; set; }
        public List<Clientes> clients { get; set; }
        public string usersphone { get; set; }
        public int statusId { get; set; }
        public int roleId   { get; set; }
    }
}
