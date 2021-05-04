using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductAgg;

namespace InventoryManagement.Application.Contracts.Inventory
{
    public class CreateInventory
    {
        [Range(1,10000,ErrorMessage = ValidationMessage.IsRequired)]
        public long ProductId { get; set; }
        [Range(1, 10000, ErrorMessage = ValidationMessage.IsRequired)]
        public double UnitPrice { get; set; }

        public List<SelectProductViewModel> Products { get; set; }
    }
}
