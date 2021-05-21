using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using AccountManagement.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;
using AccountManagement.Infrastructure;
using AccountManagement.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Configuration
{
    public class AccountManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IAccountApplication, AccountApplication>();
            services.AddTransient<IAccountRepository, AccountRepository>();

            services.AddTransient<IRoleApplication, RoleApplication>();
            services.AddTransient<IRoleRepository, RoleRepository>();


            services.AddDbContext<AccountContext>(options => options.UseSqlServer(connectionString));
        }
    }
}
