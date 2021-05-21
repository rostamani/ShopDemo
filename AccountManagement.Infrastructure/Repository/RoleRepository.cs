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
            return _db.Roles.Select(x => new EditRole
            {
                Id = x.Id,
                Name = x.Name
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);
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
