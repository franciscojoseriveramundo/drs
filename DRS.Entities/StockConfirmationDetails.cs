using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.Entities
{
    public class StockConfirmationDetails
    {
        public Int64 StockConfirmationDetailsId { get; set; }
        public Int64 StockConfirmationId { get; set; }
        public string StockConfirmationQuantity { get; set; }
        public string StockConfirmationComments { get; set; }
        public Int64 UserId { get; set; }
        public DateTime StockConfirmationDetailsCreateDate { get; set; }

    }
}
