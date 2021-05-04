using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.Application.Contracts.Inventory
{
    public class DecreaseInventory
    {
        public long InventoryId { get; set; }
        public long ProductId { get; set; }
        public long Count { get; set; }
        public long OrderId { get; set; }
        public string Description { get; set; }
    }
}
