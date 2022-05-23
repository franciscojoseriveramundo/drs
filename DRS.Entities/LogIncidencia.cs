using System;
using System.Collections.Generic;
using System.Text;

namespace DRS.Entities
{
    public class LogIncidencia
    {
        public long IncidenciaId { get; set; }
        public long UsuarioId { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
