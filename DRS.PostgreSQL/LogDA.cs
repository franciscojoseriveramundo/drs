using DRS.Data;
using DRS.Entities;
using DRS.PostgreSQL.Models;
using System;

namespace DRS.PostgreSQL
{
    public class LogDA : ILogDA
    {
        public bool CreateIncident(LogIncidencia log)
        {
            using (DRSContext dRS = new DRSContext())
            {
                Incident incident = new Incident()
                {
                    Incidentscreatedate = log.FechaCreacion,
                    Incidentsdescription = log.Descripcion,
                    Usersid = log.UsuarioId
                };

                dRS.Add(incident);

                dRS.SaveChanges();
            }

            return true;
        }

        public bool CreateTransaction(LogTransaccion log)
        {
            using (DRSContext dRS = new DRSContext())
            {
                Transaction transaction = new Transaction()
                {
                    Transactionscreatedate = log.FechaCreacion,
                    Transactionsdescription = log.Descripcion,
                    Usersid = log.UsuarioId,
                    Operationtypeid = log.TipoOperacion
                };

                dRS.Add(transaction);

                dRS.SaveChanges();
            }

            return true;
        }
    }
}
