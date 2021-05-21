using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Admin.Pages.Accounts.Role
{
    public class IndexModel : PageModel
    {
        private readonly IRoleApplication _roleApplication;

        public IndexModel(IRoleApplication roleApplication)
        {
            _roleApplication = roleApplication;
        }

        public List<RoleViewModel> Roles;
        public void OnGet()
        {
            Roles = _roleApplication.GetRoles();
        }

        public IActionResult OnGetEdit(long id)
        {
            var editAccount = _roleApplication.GetDetails(id);
            return Partial("Edit", editAccount);
        }

        public IActionResult OnGetCreate(long id)
        {
            var createRole = new CreateRole();
            return Partial("Create", createRole);
        }

        public IActionResult OnPostEdit(EditRole command)
        {
            var result = new OperationResult();
            if (ModelState.IsValid)
            {
                result = _roleApplication.Edit(command);
            }
            return new JsonResult(result);
        }

        public IActionResult OnPostCreate(CreateRole command)
        {
            var result = new OperationResult();
            if (ModelState.IsValid)
            {
                result = _roleApplication.Create(command);
            }
            return new JsonResult(result);
        }
    }
}
