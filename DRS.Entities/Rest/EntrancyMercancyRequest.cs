using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.Entities.Rest
{
    public class EntrancyMercancyRequest
    {
        public string ExternalID { get; set; }
        public string SiteID { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public string InventoryMovementDirectionCode { get; set; }
        public string CostCenterID { get; set; }
        public string IDDevPortal { get; set; }
        public bool TransactionDateTimeSpecified { get; set; } = true;
        public List<EntrancyMercancyRequestDetails> Details { get; set; }
    }
}
