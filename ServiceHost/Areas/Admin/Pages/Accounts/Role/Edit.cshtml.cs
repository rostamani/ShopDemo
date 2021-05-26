using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Admin.Pages.Accounts.Role
{
    public class EditModel : PageModel
    {
        private readonly IRoleApplication _roleApplication;
        private readonly IEnumerable<IPermissionExposer> _permissionExposer;

        public EditModel(IRoleApplication roleApplication, IEnumerable<IPermissionExposer> permissionExposer)
        {
            _roleApplication = roleApplication;
            _permissionExposer = permissionExposer;
        }

        [BindProperty]
        public EditRole Role { get; set; }

        public List<SelectListItem> Permissions;

        public void OnGet(long id)
        {
            Role = _roleApplication.GetDetails(id);
            Permissions=new List<SelectListItem>();

            foreach (var exposer in _permissionExposer)
            {
                var exposedPermission = exposer.Expose();
                foreach (var (key, value) in exposedPermission)
                {
                    var group = new SelectListGroup()
                    {
                        Name = key
                    };
                    foreach (var permission in value)
                    {
                        var item = new SelectListItem(permission.Name, permission.Code.ToString())
                        {
                            Group = group
                        };

                        if (Role.MappedPermissions.Any(x => x.Code == permission.Code))
                        {
                            item.Selected = true;
                        }

                        Permissions.Add(item);
                    }
                }
            }
        }

        public IActionResult OnPost(/*EditRole command*/)
        {
            _roleApplication.Edit(Role);
            return RedirectToPage("Index");
        }
    }
}
