using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Comment;
using ShopManagement.Application.Contracts.ProductAgg;

namespace ServiceHost.Areas.Admin.Pages.Shop.Comment
{
    public class IndexModel : PageModel
    {
        private readonly ICommentApplication _commentApplication;
        private readonly IProductApplication _productApplication;

        public IndexModel(ICommentApplication commentApplication,IProductApplication productApplication)
        {
            _commentApplication = commentApplication;
            _productApplication = productApplication;
        }

        public List<CommentViewModel> Comments;
        public CommentSearchModel SearchModel;
        public SelectList Products;

        public void OnGet(CommentSearchModel searchModel)
        {
            Comments = _commentApplication.Search(searchModel);
            Products=new SelectList(_productApplication.GetProducts(),"Id","Name");
        }

        public IActionResult OnGetCancel(long id)
        {
            var operationResult = _commentApplication.Cancel(id);
            return RedirectToPage("Index");
        }

        public IActionResult OnGetConfirm(long id)
        {
            var operationResult = _commentApplication.Confirm(id);
            return RedirectToPage("Index");
        }
    }
}
