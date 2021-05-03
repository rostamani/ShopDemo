using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductAgg;

namespace DiscountManagement.Application.Contracts.ColleagueDiscount
{
    public class DefineColleagueDiscount
    {
        [Range(1, 1000000, ErrorMessage = ValidationMessage.IsRequired)]
        public long ProductId { get; set; }
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public int DiscountRate { get; set; }
        public List<SelectProductViewModel> Products { get; set; }
    }
}
