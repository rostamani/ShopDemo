﻿using System;
using System.Collections.Generic;
using System.Text;
using _0_Framework.Domain;
using AccountManagement.Application.Contracts.Role;

namespace AccountManagement.Domain.RoleAgg
{
    public interface IRoleRepository:IRepository<long,Role>
    {
        EditRole GetDetails(long id);
        List<RoleViewModel> GetRoles();
    }
}
