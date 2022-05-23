using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.Entities
{
    public class HistorialStockConfirmation
    {
        public Int64 StockId { get; set; }
        public Int64 ReturnDetailsId { get; set; }
        public string StatusId { get; set; }
        public List<HistorialStockConfirmationDetails> StockDetails { get; set; }
    }
}
