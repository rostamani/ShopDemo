using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Admin.Pages.Accounts.Account
{
    public class IndexModel : PageModel
    {
        private readonly IAccountApplication _accountApplication;
        private readonly IRoleApplication _roleApplication;

        public IndexModel(IAccountApplication accountApplication, IRoleApplication roleApplication)
        {
            _accountApplication = accountApplication;
            _roleApplication = roleApplication;
        }

        public List<AccountViewModel> Accounts;
        public AccountSearchModel SearchModel;
        public SelectList Roles;

        public void OnGet(AccountSearchModel searchModel)
        {
            Accounts = _accountApplication.Search(searchModel);
            Roles=new SelectList(_roleApplication.GetRoles(),"Id","Name");
        }

        public IActionResult OnGetEdit(long id)
        {
            var editAccount = _accountApplication.GetDetails(id);
            editAccount.Roles = _roleApplication.GetRoles();
            return Partial("Edit", editAccount);
        }

        public IActionResult OnGetCreate(long id)
        {
            var createAccount=new CreateAccount();
            createAccount.Roles = _roleApplication.GetRoles();
            return Partial("Create",createAccount);
        }

        public IActionResult OnPostEdit(EditAccount command)
        {
            var result = new OperationResult();
            if (ModelState.IsValid)
            {
                result=_accountApplication.Edit(command);
            }
            return new JsonResult(result);
        }

        public IActionResult OnPostCreate(CreateAccount command)
        {
            var result = new OperationResult();
            if (ModelState.IsValid)
            {
                result = _accountApplication.Register(command);
            }
            return new JsonResult(result);
        }

        public IActionResult OnGetChangePassword(long id)
        {
            var changePassword = new ChangePassword(){Id = id};
            return Partial("ChangePassword", changePassword);
        }

        public IActionResult OnPostChangePassword(ChangePassword command)
        {
            var result = new OperationResult();
            if (ModelState.IsValid)
            {
                result = _accountApplication.ChangePassword(command);
            }
            return new JsonResult(result);
        }

    }
}
