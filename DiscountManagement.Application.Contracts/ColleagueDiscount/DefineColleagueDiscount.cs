using System;
using System.Collections.Generic;
using System.Text;
using ShopManagement.Application.Contracts.ProductAgg;

namespace DiscountManagement.Application.Contracts.ColleagueDiscount
{
    public class DefineColleagueDiscount
    {
        public long ProductId { get; set; }
        public int DiscountRate { get; set; }
        public List<SelectProductViewModel> Products { get; set; }
    }
}
