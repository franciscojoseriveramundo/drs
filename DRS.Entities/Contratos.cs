using System;
using System.Collections.Generic;
using System.Text;

namespace DRS.Entities
{
    public class Contratos
    {
        public Int64 IdContrato { get; set; }
        public string NombreContrato { get; set; }
        public string ClaveContrato { get; set; } 
        public Int64 IdEmpleado { get; set; }
    }
}
