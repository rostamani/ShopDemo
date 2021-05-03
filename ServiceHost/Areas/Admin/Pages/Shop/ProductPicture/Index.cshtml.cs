using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductAgg;
using ShopManagement.Application.Contracts.ProductPicture;

namespace ServiceHost.Areas.Admin.Pages.Shop.ProductPicture
{
    public class IndexModel : PageModel
    {
        private readonly IProductPictureApplication _productPictureApplication;
        private readonly IProductApplication _productApplication;

        public IndexModel(IProductPictureApplication productPictureApplication, IProductApplication productApplication)
        {
            _productPictureApplication = productPictureApplication;
            _productApplication = productApplication;
        }

        public List<ProductPictureViewModel> ProductPictures;
        public ProductPictureSearchModel SearchModel;
        public SelectList Products;

        public void OnGet(ProductPictureSearchModel searchModel)
        {
            ProductPictures = _productPictureApplication.Search(searchModel);
            Products =new SelectList(_productApplication.GetProducts(),"Id","Name");
        }

        public IActionResult OnGetCreate()
        {
            var productPicture=new CreateProductPicture();
            productPicture.Products = _productApplication.GetProducts();
            return Partial("Create", productPicture);
        }

        public IActionResult OnPostCreate(CreateProductPicture command)
        {
            var operationResult = new OperationResult();
            if (ModelState.IsValid)
            {
                operationResult = _productPictureApplication.Create(command);
            }
            return new JsonResult(operationResult);
        }

        public IActionResult OnGetEdit(long id)
        {
            var productPicture = _productPictureApplication.GetDetails(id);
            productPicture.Products = _productApplication.GetProducts();
            return Partial("Edit", productPicture);
        }

        public IActionResult OnPostEdit(EditProductPicture command)
        {
            var operationResult = new OperationResult();
            if (ModelState.IsValid)
            {
                operationResult= _productPictureApplication.Edit(command);
            }
            return new JsonResult(operationResult);
        }

        public IActionResult OnGetRemove(long id)
        {
            _productPictureApplication.Remove(id);
            return RedirectToPage("Index");
        }

        public IActionResult OnGetRestore(long id)
        {
            _productPictureApplication.Restore(id);
            return RedirectToPage("Index");
        }

    }
}
