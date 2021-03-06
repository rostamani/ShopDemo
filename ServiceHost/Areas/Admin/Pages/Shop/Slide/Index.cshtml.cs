using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Configuration.Permissions;


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

        [NeedsPermission(ShopPermissions.ListSlides)]
        public void OnGet()
        {
            Slides = _slideApplication.Search();
        }

        [NeedsPermission(ShopPermissions.CreateSlide)]
        public IActionResult OnGetCreate()
        {
            return Partial("Create", new CreateSlide());
        }

        [NeedsPermission(ShopPermissions.CreateSlide)]
        public IActionResult OnPostCreate(CreateSlide command)
        {
            var operationResult=new OperationResult();
            if (ModelState.IsValid)
            {
                operationResult = _slideApplication.Create(command);
                
            }
            return new JsonResult(operationResult);

        }
        [NeedsPermission(ShopPermissions.EditSlide)]
        public IActionResult OnGetEdit(long id)
        {
            var editSlide = _slideApplication.GetDetails(id);
            return Partial("Edit", editSlide);
        }

        [NeedsPermission(ShopPermissions.EditSlide)]
        public IActionResult OnPostEdit(EditSlide command)
        {
            var operationResult=new OperationResult();
            if (ModelState.IsValid)
            {
                operationResult = _slideApplication.Edit(command);
            }
            return new JsonResult(operationResult);

        }
        [NeedsPermission(ShopPermissions.RemoveSlide)]
        public IActionResult OnGetRemove(long id)
        {
            var operationResult = _slideApplication.Remove(id);
            return RedirectToPage("Index");
        }
        [NeedsPermission(ShopPermissions.RestoreSlide)]
        public IActionResult OnGetRestore(long id)
        {
            var operationResult = _slideApplication.Restore(id);
            return RedirectToPage("Index");
        }

    }
}
