using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductAgg;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Configuration.Permissions;

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

        [NeedsPermission(ShopPermissions.ListProducts)]
        public void OnGet(ProductSearchModel searchModel)
        {
            Products = _productApplication.Search(searchModel);
            Categories=new SelectList(_productCategoryApplication.GetCategories(),"Id","Name");
        }

        [NeedsPermission(ShopPermissions.EditProduct)]
        public IActionResult OnGetEdit(long id)
        {
            var product = _productApplication.GetDetails(id);
            product.Categories = _productCategoryApplication.GetCategories();
            return Partial("Edit", product);
        }

        [NeedsPermission(ShopPermissions.EditProduct)]
        public IActionResult OnPostEdit(EditProduct command)
        {
            var result = new OperationResult();
            if (ModelState.IsValid)
            {
                result = _productApplication.Edit(command);
            }
            return new JsonResult(result);
        }

        [NeedsPermission(ShopPermissions.CreateProduct)]
        public IActionResult OnGetCreate()
        {
            var product = new CreateProduct();
            product.Categories = _productCategoryApplication.GetCategories();
            return Partial("Create",product );
        }

        [NeedsPermission(ShopPermissions.CreateProduct)]
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
