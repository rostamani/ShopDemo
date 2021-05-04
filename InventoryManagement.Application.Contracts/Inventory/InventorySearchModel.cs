using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.Application.Contracts.Inventory
{
    public class InventorySearchModel
    {
        public long ProductId { get; set; }
        public bool IsNotInStock { get; set; }

    }
}
