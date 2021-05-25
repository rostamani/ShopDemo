using System;
using System.Collections.Generic;
using System.Text;
using _0_Framework.Application;
using _0_Framework.Domain;

namespace AccountManagement.Application.Contracts.Account
{
    public interface IAccountApplication
    {
        OperationResult Register(CreateAccount command);
        OperationResult Edit(EditAccount command);
        OperationResult ChangePassword(ChangePassword command);
        OperationResult Login(LoginModel command);
        void Logout();
        List<AccountViewModel> Search(AccountSearchModel searchModel);
        EditAccount GetDetails(long id);
    }
}
