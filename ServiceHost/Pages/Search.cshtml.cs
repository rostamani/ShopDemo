using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _01_ShopQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class SearchModel : PageModel
    {
        private readonly IProductQuery _productQuery;
        public string Value;

        public SearchModel(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public List<ProductQueryModel> Products;

        public void OnGet(string value)
        {
            Products = _productQuery.Search(value);
            Value = value;
        }
    }
}
