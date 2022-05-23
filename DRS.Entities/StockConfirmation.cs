using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.Entities
{
    public class StockConfirmation
    {
        public Int64 StockId { get; set; }
        public Int64 ReturnDetailsId { get; set; }
        public int StatusId { get; set; }
        public List<StockConfirmationDetails> StockDetails { get; set; }
    }
}
