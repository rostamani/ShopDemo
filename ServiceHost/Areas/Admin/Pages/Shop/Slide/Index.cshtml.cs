using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Slide;


namespace ServiceHost.Areas.Admin.Pages.Shop.Slide
{
    public class IndexModel : PageModel
    {
        private readonly ISlideApplication _slideApplication;

        public IndexModel(ISlideApplication slideApplication)
        {
            _slideApplication = slideApplication;
        }

        public List<SlideViewModel> Slides;

        public void OnGet()
        {
            Slides = _slideApplication.Search();
        }

        public IActionResult OnGetCreate()
        {
            return Partial("Create", new CreateSlide());
        }

        public IActionResult OnPostCreate(CreateSlide command)
        {
            if (ModelState.IsValid)
            {
                var operationResult = _slideApplication.Create(command);
                return new JsonResult(operationResult);
            }

            return RedirectToPage("Index");

        }

        public IActionResult OnGetEdit(long id)
        {
            var editSlide = _slideApplication.GetDetails(id);
            return Partial("Edit", editSlide);
        }

        public IActionResult OnPostEdit(EditSlide command)
        {
            if (ModelState.IsValid)
            {
                var operationResult = _slideApplication.Edit(command);
                return new JsonResult(operationResult);
            }

            return RedirectToPage("Index");

        }

        public IActionResult OnGetRemove(long id)
        {
            var operationResult = _slideApplication.Remove(id);
            return RedirectToPage("Index");
        }

        public IActionResult OnGetRestore(long id)
        {
            var operationResult = _slideApplication.Restore(id);
            return RedirectToPage("Index");
        }

    }
}
