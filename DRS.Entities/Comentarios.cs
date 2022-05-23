using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.Entities
{
    public class Comentarios
    {
        public long ComentariosId { get; set; }
        public long UsuarioId { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreación { get; set; }
        public string PersonName { get; set; }
        
    }
}
