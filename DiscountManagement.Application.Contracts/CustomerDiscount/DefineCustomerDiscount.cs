using System;
using System.Collections.Generic;
using System.Text;
using ShopManagement.Application.Contracts.ProductAgg;

namespace DiscountManagement.Application.Contracts.CustomerDiscount
{
    public class DefineCustomerDiscount
    {
        public int ProductId { get;  set; }
        public int DiscountRate { get;  set; }
        public string Reason { get;  set; }
        public string StartDate { get;  set; }
        public string EndDate { get;  set; }
        public List<SelectProductViewModel> Products { get; set; }
    }
}
