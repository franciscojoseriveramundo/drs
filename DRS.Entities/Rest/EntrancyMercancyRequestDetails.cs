using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.Entities.Rest
{
    public class EntrancyMercancyRequestDetails
    {
        public string ExternalItemID { get; set; }
        public string MaterialInternalID { get; set; }
        public string OwnerPartyInternalID { get; set; }
        public bool InventoryRestrictedUseIndicator { get; set; }
        public string LogisticsAreaID { get; set; }
        public decimal InventoryItemChangeQuantity { get; set; }
        public List<string> InventoryItemChangeSerialNumber { get; set; }
        public string PosDevPortal { get; set; }
    }
}
