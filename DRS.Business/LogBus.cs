using DRS.Business.Functions;
using DRS.Data;
using DRS.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DRS.Business
{
    public class LogBus
    {
        private readonly ILogDA iLogDA;

        public LogBus(ILogDA iLogDA)
        {
            this.iLogDA = iLogDA;
        }

        public bool CreateIncident(LogIncidencia log)
        {
            //log.FechaCreacion = DateTimeHelper.getCurrentDateTimeWithTimeZone(log.FechaCreacion);
            return iLogDA.CreateIncident(log);
        }

        public bool CreateTransaction(LogTransaccion log)
        {
            //log.FechaCreacion = DateTimeHelper.getCurrentDateTimeWithTimeZone(log.FechaCreacion);
            return iLogDA.CreateTransaction(log);
        }
    }
}
