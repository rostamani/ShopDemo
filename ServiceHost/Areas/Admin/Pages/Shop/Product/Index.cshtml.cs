using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductAgg;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Admin.Pages.Shop.Product
{
    public class IndexModel : PageModel
    {
        private readonly IProductApplication _productApplication;
        private readonly IProductCategoryApplication _productCategoryApplication;

        public IndexModel(IProductApplication productApplication, IProductCategoryApplication productCategoryApplication)
        {
            _productApplication = productApplication;
            _productCategoryApplication = productCategoryApplication;
        }

        public List<ProductViewModel> Products;
        public ProductSearchModel SearchModel;
        public SelectList Categories;

        public void OnGet(ProductSearchModel searchModel)
        {
            Products = _productApplication.Search(searchModel);
            Categories=new SelectList(_productCategoryApplication.GetCategories(),"Id","Name");
        }

        public IActionResult OnGetEdit(long id)
        {
            var product = _productApplication.GetDetails(id);
            product.Categories = _productCategoryApplication.GetCategories();
            return Partial("Edit", product);
        }

        public IActionResult OnPostEdit(EditProduct command)
        {
            var result = new OperationResult();
            if (ModelState.IsValid)
            {
                result = _productApplication.Edit(command);
            }
            return new JsonResult(result);
        }

        public IActionResult OnGetCreate()
        {
            var product = new CreateProduct();
            product.Categories = _productCategoryApplication.GetCategories();
            return Partial("Create",product );
        }

        public IActionResult OnPostCreate(CreateProduct command)
        {
            var result = new OperationResult();
            if (ModelState.IsValid)
            {
                result = _productApplication.Create(command);
            }
            return new JsonResult(result);
        }

    }
}
