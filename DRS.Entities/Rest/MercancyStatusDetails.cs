using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.Entities.Rest
{
    public class MercancyStatusDetails
    {
        public DateTime MercancyTransactionDate { get; set; }
        public DateTime MercancyCreateDate { get; set; }
        public int MercancyStatusId { get; set; }
        public string ChangeReasonCode { get; set; }
        public string CompanyId { get; set; }
        public string UUID { get; set; }
        public decimal Quantity { get; set; }
        public string UnitCode { get; set; }
        public string DocumentId { get; set; }
        public string DirectionCode { get; set; }
        public List<string> Serials { get; set; }
    }
}
