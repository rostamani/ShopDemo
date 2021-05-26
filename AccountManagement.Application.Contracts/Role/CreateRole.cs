using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using _0_Framework.Application;
using _0_Framework.Infrastructure;

namespace AccountManagement.Application.Contracts.Role
{
    public class CreateRole
    {
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(50, ErrorMessage = ValidationMessage.MaxLength)]
        public string Name { get; set; }

        public List<int> Permissions { get; set; }

    }
}
