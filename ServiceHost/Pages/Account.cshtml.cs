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

        [TempData]
        public string RegisterMessage { get; set; }

        [BindProperty]
        public LoginModel LoginModel { get; set; }

        [BindProperty]
        public RegisterAccount RegisterAccount { get; set; }

        public void OnGet()
        {
            LoginModel = new LoginModel();
        }

        public IActionResult OnPostLogin()
        {
            var operationResult = new OperationResult();

            operationResult = _accountApplication.Login(LoginModel);
            if (operationResult.IsSucceeded)
            {
                return RedirectToPage("./Index");
            }

            LoginMessage = operationResult.Message;

            return Page();
        }

        public IActionResult OnPostRegister()
        {
            var operationResult = new OperationResult();

            var createAccount = new CreateAccount()
            {
                Username = RegisterAccount.Username,
                Password = RegisterAccount.Password,
                RoleId = 2,
                Mobile = RegisterAccount.Mobile,
                Fullname = RegisterAccount.Fullname,
            };
            operationResult = _accountApplication.Register(createAccount);
            if (!operationResult.IsSucceeded)
                RegisterMessage = operationResult.Message;


            return Page();
        }

        public IActionResult OnGetLogout()
        {
            _accountApplication.Logout();
            return RedirectToPage("Index");
        }
    }
}
