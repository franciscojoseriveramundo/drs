using System;
using System.Collections.Generic;
using System.Text;

namespace DRS.Entities
{
    public class DetallesDevolucion
    {
        public long Id { get; set; }
        public long IdDetallesDevolucion { get; set; }
        public bool EsSoloDevolucion { get; set; }
        public bool ExisteProducto { get; set; }
        public int MotivoEnvio { get; set; }
        public long TipoProducto { get; set; }
        public string Producto { get; set; }
        public string Cantidad { get; set; }
        public string Serie { get; set; }
        public string SIM { get; set; }
        public string Dn { get; set; }
        public bool Devolucion { get; set; }
        //public string Contrato { get; set; }
        public int Antena { get; set; }
        public int Carcasa { get; set; }
        public int Display { get; set; }
        public int ConectorUSB { get; set; }
        public int Teclado { get; set; }
        public string Otro { get; set; }
        public int Tapa { get; set; }
        public int Bateria { get; set; }
        public int CargadorEliminador { get; set; }
        public int CableUSB { get; set; }
        public int CableUSBMagnetico { get; set; }
        public int BaseCarga { get; set; }
        public int Clip { get; set; } 
        public int Manual { get; set; }
        public int Caja { get; set; }
        public int HerramientaDeExtraccion { get; set; }
        /*TeamVox*/
        #region TeamVox

        public string Alias { get; set; }
        public string Grupo { get; set; }
        public bool SimPropiedadServitron { get; set; }
        public bool TeamVoxLiteAbierto { get; set; }
        public bool TeamVoxModoSeguro { get; set; }
        public bool EvidenceLite { get; set; }
        public bool EvidenceForms { get; set; }
        public bool Zaypher { get; set; }
        public bool TeamVoxDispatch { get; set; }
        public int PlanId { get; set; }
        public int CarrierId { get; set; }

        #endregion

        /*Legacy*/
        #region Legacy

        public string Leyenda { get; set; }
        public string NombreCarpeta { get; set; }
        public bool IdentificadorLlamadas { get; set; }
        public bool GPS { get; set; }
        public bool LlamadaPrivada { get; set; }
        public string Grupos { get; set; }
        public string SitiosSuscribe { get; set; }

        #endregion

        public string Observaciones { get; set; }
        public long QuantityRestante { get; set; }

        //StockConfirmation
        public StockConfirmation StockConfirmation { get; set; }

        //DetailsReservationStock
        public DevolucionesDetailsStock DetailsReservationStock { get; set; }
    }
}
