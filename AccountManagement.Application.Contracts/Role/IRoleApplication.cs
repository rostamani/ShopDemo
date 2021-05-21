using System;
using System.Collections.Generic;
using System.Text;
using _0_Framework.Application;
using _0_Framework.Domain;

namespace AccountManagement.Application.Contracts.Role
{
    public interface IRoleApplication
    {
        OperationResult Create(CreateRole command);
        OperationResult Edit(EditRole command);
        EditRole GetDetails(long id);
        List<RoleViewModel> GetRoles();
    }
}
