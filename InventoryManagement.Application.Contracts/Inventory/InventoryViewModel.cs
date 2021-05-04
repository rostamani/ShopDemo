using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.Application.Contracts.Inventory
{
    public class InventoryViewModel
    {
        public long Id { get; set; }
        public string Product { get; set; }
        public long ProductId { get; set; }
        public double UnitPrice { get; set; }
        public long CurrentCount { get; set; }
        public bool IsInStock { get; set; }
    }
}
