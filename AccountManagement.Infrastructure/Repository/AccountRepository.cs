using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.Repository
{
    public class AccountRepository:RepositoryBase<long,Account>,IAccountRepository
    {
        private readonly AccountContext _db;

        public AccountRepository(AccountContext db) : base(db)
        {
            _db = db;
        }

        public EditAccount GetDetails(long id)
        {
            return _db.Accounts.Select(x => new EditAccount
            {
                Id = x.Id,
                Fullname = x.Fullname,
                Mobile = x.Mobile,
                RoleId = x.RoleId,
                Username = x.Username
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            var result=_db.Accounts.Include(x=>x.Role).Select(x => new AccountViewModel
            {
                Id = x.Id,
                Fullname = x.Fullname,
                Username = x.Username,
                Mobile = x.Mobile,
                ProfilePhoto = x.ProfilePhoto,
                Role = x.Role.Name,
                RegisteredDate = x.CreationDate.ToFarsi()
            }).AsNoTracking();

            if (searchModel.RoleId!=0)
            {
                result = result.Where(x => x.RoleId == searchModel.RoleId);
            }

            if (!string.IsNullOrWhiteSpace(searchModel.Fullname))
            {
                result = result.Where(x => x.Fullname.Contains(searchModel.Fullname));
            }

            if (!string.IsNullOrWhiteSpace(searchModel.Mobile))
            {
                result = result.Where(x => x.Fullname.Contains(searchModel.Mobile));
            }

            if (!string.IsNullOrWhiteSpace(searchModel.Username))
            {
                result = result.Where(x => x.Username.Contains(searchModel.Username));
            }

            return result.OrderByDescending(x => x.Id).ToList();
        }

        public Account GetBy(string username)
        {
            return _db.Accounts.FirstOrDefault(x => x.Username==username);
        }
    }
}
