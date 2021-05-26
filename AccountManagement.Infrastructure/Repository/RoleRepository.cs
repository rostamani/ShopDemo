using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.Repository
{
    public class RoleRepository:RepositoryBase<long, Role>,IRoleRepository
    {
        private readonly AccountContext _db;

        public RoleRepository(AccountContext db) : base(db)
        {
            _db = db;
        }

        public EditRole GetDetails(long id)
        {
            var role= _db.Roles.Select(x => new EditRole
            {
                Id = x.Id,
                Name = x.Name,
                MappedPermissions=MapPermissions(x.Permissions)
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);

            if (role!=null)
            {
                role.Permissions = role.MappedPermissions.Select(x => x.Code).ToList();
            }
            return role;
        }

        private static List<PermissionDto> MapPermissions(List<Permission> permissions)
        {
            return permissions.Select(x => new PermissionDto(x.Name, x.Code)).ToList();
        }

        public List<RoleViewModel> GetRoles()
        {
            return _db.Roles.Select(x => new RoleViewModel()
            {
                Name = x.Name,
                Id = x.Id,
                CreationDate = x.CreationDate.ToFarsi()
            }).AsNoTracking().ToList();
        }
    }
}
