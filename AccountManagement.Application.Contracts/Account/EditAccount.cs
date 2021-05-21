using System;
using System.Collections.Generic;
using System.Text;

namespace AccountManagement.Application.Contracts.Account
{
    public class EditAccount:CreateAccount
    {
        public long Id { get; set; }
    }
}
