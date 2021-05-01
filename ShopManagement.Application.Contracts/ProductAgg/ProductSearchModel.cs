using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Application.Contracts.ProductAgg
{
    public class ProductSearchModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int CategoryId { get; set; }

    }
}
