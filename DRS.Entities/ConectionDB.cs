using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.Entities
{
    public static class ConectionDB
    {
        public static string ConnectionString { get; set; }
        public static IConfiguration Configuration { get; set; }
        public static string EmployeeWS { get; set; }
    }
}
