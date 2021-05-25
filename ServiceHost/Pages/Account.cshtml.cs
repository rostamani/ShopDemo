using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class AccountModel : PageModel
    {
        private readonly IAccountApplication _accountApplication;

        public AccountModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }


        [TempData]
        public string LoginMessage { get; set; }

        public void OnGet()
        {
            
        }

        public IActionResult OnPostLogin(LoginModel command)
        {
            var operationResult = new OperationResult();
            if (ModelState.IsValid)
            {
                operationResult=_accountApplication.Login(command);
                if (operationResult.IsSucceeded)
                {
                     return RedirectToPage("./Index");
                }

                LoginMessage = operationResult.Message;
            }

            return Page();
        }

        public IActionResult OnGetLogout()
        {
            _accountApplication.Logout();
            return RedirectToPage("Index");
        }
    }
}
