using System;
using System.Collections.Generic;
using System.Text;

namespace _0_Framework.Application
{
    public interface IAuthHelper
    {
        void SignOut();
        void SignIn(AuthViewModel account);
        bool IsAuthenticated();
        string GetCurrentUserRole();
        AuthViewModel GetCurrentUserInfo();
    }
}
