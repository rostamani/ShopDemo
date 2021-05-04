using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.Application.Contracts.Inventory
{
    public class EditInventory:CreateInventory
    {
        public long Id { get; set; }
    }
}
