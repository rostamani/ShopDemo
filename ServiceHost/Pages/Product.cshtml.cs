using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _01_ShopQuery.Contracts.Product;
using _01_ShopQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Comment;

namespace ServiceHost.Pages
{
    public class ProductModel : PageModel
    {
        private readonly IProductQuery _productQuery;
        private readonly ICommentApplication _commentApplication;

        public ProductModel(IProductQuery productQuery,ICommentApplication commentApplication)
        {
            _productQuery = productQuery;
            _commentApplication = commentApplication;
        }

        public ProductQueryModel Product;

        public void OnGet(string id)
        {
            Product = _productQuery.GetDetails(id);
        }

        public IActionResult OnPost(AddComment comment,string productSlug)
        {
            var operationResult = _commentApplication.AddComment(comment);
            return RedirectToPage("Product", new {id = productSlug});
        }
    }
}
