using Microsoft.Extensions.DependencyInjection;
using System;
using DiscountManagement.Application;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using DiscountManagement.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace DiscountManagement.Infrastructure.Configuration
{
    public class DiscountManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<ICustomerDiscountRepository, CustomerDiscountRepository>();
            services.AddTransient<ICustomerDiscountApplication, CustomerDiscountApplication>();

            services.AddDbContext<DiscountContext>(options => { options.UseSqlServer(connectionString); });
        }
    }
}
