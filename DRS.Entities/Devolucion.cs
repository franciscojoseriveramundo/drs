using System;
using System.Collections.Generic;
using System.Text;

namespace DRS.Entities
{
    public class Devolucion
    {
        public Int64 ClaveDevolucion { get; set; } = 0;
        public string NumeroGuia { get; set; }
        public Int64 Cliente { get; set; }
        //public Contrato Contrato { get; set; }
        public string Descripcion { get; set; }
        public Int64 EmpleadoResponsable { get; set; }
        public string NombreMensajeria { get; set; }
        public string DestinoCC { get; set; }
        public string Envio { get; set; }
        public bool Cotizar { get; set; }
        public List<Comentarios> Comentarios { get; set; }
        public string Comentario { get; set; }
        public bool IsBtnContinueEnabled { get; set; }
        public List<DetallesDevolucion> Details { get; set; }
        public int MotivoEnvio { get; set; }
        public int Estatus { get; set; }
        public DateTime FechaCreacion { get; set; }
        public long UserId { get; set; }
        public long ConfirmaRechazada { get; set; }
        
        
    }
}
