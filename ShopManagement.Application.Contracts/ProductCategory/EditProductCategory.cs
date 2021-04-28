using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Application.Contracts.ProductCategory
{
    public class EditProductCategory:CreateProductCategory
    {
        public long Id { get; set; }
    }
}
