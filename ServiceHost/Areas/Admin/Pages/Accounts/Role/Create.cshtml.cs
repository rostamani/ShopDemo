using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Admin.Pages.Accounts.Role
{
    public class CreateModel : PageModel
    {
        private readonly IRoleApplication _roleApplication;

        public CreateModel(IRoleApplication roleApplication)
        {
            _roleApplication = roleApplication;
        }

        public CreateRole Command;

        public void OnGet()
        {
            Command=new CreateRole();
        }

        public IActionResult OnPost(CreateRole command)
        {
            _roleApplication.Create(command);
            return RedirectToPage("Index");
        }
    }
}
