using System;
using System.Collections.Generic;
using System.Text;

namespace AccountManagement.Application.Contracts.Account
{
    public class AccountSearchModel
    {
        public string Username { get; set; }
        public string Mobile { get; set; }
        public string Fullname { get; set; }
        public long RoleId { get; set; }
    }
}
