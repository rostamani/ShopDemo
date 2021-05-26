using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using _0_Framework.Application;

namespace AccountManagement.Application.Contracts.Account
{
    public class RegisterAccount
    {
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Username { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Fullname { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Password { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [Compare("Password",ErrorMessage = ValidationMessage.ComparePassword)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Mobile { get; set; }
    }
}
