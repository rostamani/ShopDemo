using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Configuration.Permissions;

namespace ServiceHost.Areas.Admin.Pages.Shop.ProductCategory
{
    public class IndexModel : PageModel
    {
        private readonly IProductCategoryApplication _productCategoryApplication;

        public IndexModel(IProductCategoryApplication productCategoryApplication)
        {
            _productCategoryApplication = productCategoryApplication;
        }


        public ProductCategorySearchModel SearchModel;

        public List<ProductCategoryViewModel> ProductCategories { get; set; }

        [NeedsPermission(ShopPermissions.ListProductCategories)]
        public void OnGet(ProductCategorySearchModel searchModel)
        {
            ProductCategories = _productCategoryApplication.Search(searchModel);
        }

        [NeedsPermission(ShopPermissions.CreateProductCategory)]
        public IActionResult OnGetCreate()
        {
            return Partial("Create", new CreateProductCategory());
        }

        [NeedsPermission(ShopPermissions.CreateProductCategory)]
        public IActionResult OnPostCreate(CreateProductCategory command)
        {
            var result = new OperationResult();
            if (ModelState.IsValid)
            {
                result=_productCategoryApplication.Create(command);
            }
            return new JsonResult(result);
        }

        [NeedsPermission(ShopPermissions.EditProductCategory)]
        public IActionResult OnGetEdit(long id)
        {
            return Partial("Edit", _productCategoryApplication.GetDetails(id));
        }

        [NeedsPermission(ShopPermissions.EditProductCategory)]
        public IActionResult OnPostEdit(EditProductCategory command)
        {
            var result=new OperationResult();
            if (ModelState.IsValid)
            {
                result = _productCategoryApplication.Edit(command);
            }
            return new JsonResult(result);
        }
    }
}
