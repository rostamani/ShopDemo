using System;
using System.Collections.Generic;
using System.Text;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;
using AccountManagement.Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure
{
    public class AccountContext:DbContext
    {
        public AccountContext(DbContextOptions<AccountContext> options):base(options)
        {
            
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(AccountMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }
    }
}
