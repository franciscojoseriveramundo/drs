using System;
using System.Collections.Generic;
using System.Text;

namespace DRS.Entities
{
    public class DevolucionesViewModel
    {
        public string Clientname { get; set; }
        public string Employeeassigned { get; set; }
        public string Returnssendedfor { get; set; }
        public int Reasonsendid { get; set; }
        public string Returnstatusname { get; set; }
        public string Reasonsendname { get; set; }
        public Int64 Returnsid { get; set; }
        public Int64 Clientid { get; set; }
    }
}
