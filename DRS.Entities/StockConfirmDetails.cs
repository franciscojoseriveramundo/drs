using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.Entities
{
    public class StockConfirmDetails
    {
        public long StockConfirmDetailsId { get; set; }
        public int StockConfirmOrderId { get; set; }
        public long ProductId { get; set; }
        public long ReturnDetailsId { get; set; }
        public long StockConfirmId { get; set; }
        public long StockConfirmationid { get; set; }
        public long StockConfirmationDetailsId { get; set; }
        public int StockQuantity { get; set; }
    }
}
