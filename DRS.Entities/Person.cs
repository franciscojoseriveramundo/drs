using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.Entities
{
    public class Person
    {
        public long PersonId { get; set; }
        public string Personname { get; set; }
        public string Personlastname { get; set; }
        public int UserRoleId { get; set; }
        public MultiSelectDropDownEmployee EmployeeId { get; set; }
        public List<PersonEmployee> Personemployees { get; set; }
        public Users Users { get; set; }
        public Account Account { get; set; }
        public RAprofile Raprofile { get; set; }
    }
}
