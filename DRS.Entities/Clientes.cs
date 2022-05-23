using System;
using System.Collections.Generic;
using System.Text;

namespace DRS.Entities
{
    public class Clientes
    {
        public Int64 IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public string ClaveCliente { get; set; }
        public string ClienteEstatus { get; set; }
        public Int64 PersonaId { get; set; }
        public Int64 EmployeeId { get; set; }
    }
}
