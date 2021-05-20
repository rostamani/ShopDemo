using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _01_ShopQuery.Contracts.Product;
using _01_ShopQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductModel : PageModel
    {
        private readonly IProductQuery _productQuery;

        public ProductModel(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public ProductQueryModel Product;

        public void OnGet(string id)
        {
            Product = _productQuery.GetDetails(id);
        }
    }
}
