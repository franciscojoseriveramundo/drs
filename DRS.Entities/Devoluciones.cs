using System;
using System.Collections.Generic;
using System.Text;

namespace DRS.Entities
{
    public class Devoluciones
    {
        public string NoInterno { get; set; }
        public string Cliente { get; set; }
        //public string Contrato { get; set; }
        public string EmpleadoResponsable { get; set; }
        public string EnviadoPor { get; set; }
        public string MotivoEnvio { get; set; }
        public string Estatus { get; set; }
        public Int32 MotivoEnvioId { get; set; }
        public Int64 ClienteId { get; set; }
    }
}
