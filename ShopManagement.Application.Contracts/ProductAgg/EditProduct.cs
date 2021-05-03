using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.ProductAgg
{
    public class EditProduct:CreateProduct
    {
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public long Id { get; set; }

    }
}
