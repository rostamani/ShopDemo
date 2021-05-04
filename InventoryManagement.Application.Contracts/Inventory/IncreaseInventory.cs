using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using _0_Framework.Application;

namespace InventoryManagement.Application.Contracts.Inventory
{
    public class IncreaseInventory
    {
        public long InventoryId { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public long Count { get; set; }

    }
}
