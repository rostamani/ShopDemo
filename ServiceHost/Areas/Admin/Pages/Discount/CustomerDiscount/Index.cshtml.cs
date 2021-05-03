using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Application;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductAgg;

namespace ServiceHost.Areas.Admin.Pages.Discount.CustomerDiscount
{
    public class IndexModel : PageModel
    {
        private readonly IProductApplication _productApplication;
        private readonly ICustomerDiscountApplication _customerDiscountApplication;

        public IndexModel(IProductApplication productApplication, ICustomerDiscountApplication customerDiscountApplication)
        {
            _productApplication = productApplication;
            _customerDiscountApplication = customerDiscountApplication;
        }

        public CustomerDiscountSearchModel SearchModel;
        public SelectList Products;
        public List<CustomerDiscountViewModel> CustomerDiscounts;
        public void OnGet(CustomerDiscountSearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(),"Id","Name");
            CustomerDiscounts = _customerDiscountApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var defineCustomerDiscount = new DefineCustomerDiscount();
            defineCustomerDiscount.Products = _productApplication.GetProducts();
            return Partial("Create", defineCustomerDiscount);
        }

        public IActionResult OnPostCreate(DefineCustomerDiscount command)
        {
            var result = new OperationResult();
            if (ModelState.IsValid)
            {
                result=_customerDiscountApplication.Define(command);
            }

            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var defineCustomerDiscount = _customerDiscountApplication.GetDetails(id);
            defineCustomerDiscount.Products = _productApplication.GetProducts();
            return Partial("Edit", defineCustomerDiscount);
        }

        public IActionResult OnPostEdit(EditCustomerDiscount command)
        {
            var result = new OperationResult();
            if (ModelState.IsValid)
            {
                result = _customerDiscountApplication.Edit(command);
            }

            return new JsonResult(result);
        }

        public IActionResult OnGetRemove(long id)
        {
            _customerDiscountApplication.Remove(id);
            return RedirectToPage("Index");
        }

        public IActionResult OnGetRestore(long id)
        {
            _customerDiscountApplication.Restore(id);
            return RedirectToPage("Index");
        }
    }
}
