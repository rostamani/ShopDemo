using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using _0_Framework.Application;

namespace AccountManagement.Application.Contracts.Account
{
    public class LoginModel
    {
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Username { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
