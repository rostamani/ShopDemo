using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Application;
using DiscountManagement.Application.Contracts.ColleagueDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductAgg;

namespace ServiceHost.Areas.Admin.Pages.Discount.ColleagueDiscount
{
    public class IndexModel : PageModel
    {
        private readonly IProductApplication _productApplication;
        private readonly IColleagueDiscountApplication _colleagueDiscountApplication;

        public IndexModel(IProductApplication productApplication, IColleagueDiscountApplication colleagueDiscountApplication)
        {
            _productApplication = productApplication;
            _colleagueDiscountApplication = colleagueDiscountApplication;
           
        }

        public ColleagueDiscountSearchModel SearchModel;
        public SelectList Products;
        public List<ColleagueDiscountViewModel> ColleagueDiscounts;
        public void OnGet(ColleagueDiscountSearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            ColleagueDiscounts = _colleagueDiscountApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var defineColleagueDiscount = new DefineColleagueDiscount();
            defineColleagueDiscount.Products = _productApplication.GetProducts();
            return Partial("Create", defineColleagueDiscount);
        }

        public IActionResult OnPostCreate(DefineColleagueDiscount command)
        {
            var result = new OperationResult();
            if (ModelState.IsValid)
            {
                result = _colleagueDiscountApplication.Define(command);
            }

            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var editColleagueDiscount = _colleagueDiscountApplication.GetDetails(id);
            editColleagueDiscount.Products = _productApplication.GetProducts();
            return Partial("Edit", editColleagueDiscount);
        }

        public IActionResult OnPostEdit(EditColleagueDiscount command)
        {
            var result = new OperationResult();
            if (ModelState.IsValid)
            {
                result = _colleagueDiscountApplication.Edit(command);
            }

            return new JsonResult(result);
        }

        public IActionResult OnGetRemove(long id)
        {
            _colleagueDiscountApplication.Remove(id);
            return RedirectToPage("Index");
        }

        public IActionResult OnGetRestore(long id)
        {
            _colleagueDiscountApplication.Restore(id);
            return RedirectToPage("Index");
        }
    }
}
