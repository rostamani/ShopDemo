using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Admin.Pages.Shop.ProductCategory
{
    public class IndexModel : PageModel
    {
        private readonly IProductCategoryApplication _productCategoryApplication;

        public IndexModel(IProductCategoryApplication productCategoryApplication)
        {
            _productCategoryApplication = productCategoryApplication;
        }

        public List<ProductCategoryViewModel> ProductCategories { get; set; }

        public ProductCategorySearchModel SearchModel { get; set; }

        public void OnGet(ProductCategorySearchModel searchModel)
        {
            ProductCategories = _productCategoryApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            return Partial("Create", new CreateProductCategory());
        }

        public IActionResult OnPostCreate(CreateProductCategory command)
        {
            var operationResult = _productCategoryApplication.Create(command);
            return new JsonResult(operationResult);
        }

        public IActionResult OnGetEdit(long id)
        {
            var productCategory = _productCategoryApplication.GetDetails(id);
            return Partial("Edit", productCategory);
        }

        public IActionResult OnPostEdit(EditProductCategory command)
        {
            var operationResult = _productCategoryApplication.Edit(command);
            return new JsonResult(operationResult);
        }


    }
}
