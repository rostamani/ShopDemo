using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Http;

namespace AccountManagement.Application.Contracts.Account
{
    public class CreateAccount
    {
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(100,ErrorMessage = ValidationMessage.MaxLength)]
        public string Fullname { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(100, ErrorMessage = ValidationMessage.MaxLength)]
        public string Username { get; set; }

        //[Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(100, ErrorMessage = ValidationMessage.MaxLength)]
        public string Password { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(20, ErrorMessage = ValidationMessage.MaxLength)]
        public string Mobile { get; set; }

        [Range(1,1000,ErrorMessage = ValidationMessage.IsRequired)]
        public long RoleId { get; set; }

        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessage.MaxFileSizeError)]
        [FileExtensionLimit(new string[] { ".jpg", ".jpeg", ".png" }, ErrorMessage = ValidationMessage.InvalidFileFormat)]
        public IFormFile ProfilePhoto { get; set; }
        public List<RoleViewModel> Roles { get; set; }
    }
}
