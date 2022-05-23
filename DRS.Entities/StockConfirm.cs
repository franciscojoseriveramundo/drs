using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.Entities
{
    public class StockConfirm
    {
        public long StockConfirmId { get; set; }
        public DateTime? StockConfirmCreateDate { get; set; }
        public int StatusConfirmStatusId { get; set; }
        public string StockConfirmSapCode { get; set; }
        public string StockConfirmSaUuid { get; set; }
        public long ReturnId { get; set; }
        public List<StockConfirmDetails> Details { get; set; }
    }
}
