using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.Application.Contracts.Inventory
{
    public class IncreaseInventory
    {
        public long InventoryId { get; set; }
        public string Description { get; set; }
        public long Count { get; set; }

    }
}
