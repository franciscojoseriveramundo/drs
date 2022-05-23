using DRS.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DRS.Data
{
    public interface ILogDA
    {
        public bool CreateTransaction(LogTransaccion log);
        public bool CreateIncident(LogIncidencia log);
    }
}
