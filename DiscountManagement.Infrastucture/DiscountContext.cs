using System;
using System.Collections.Generic;
using System.Text;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using DiscountManagement.Domain.CustomerDiscountAgg;
using DiscountManagement.Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;

namespace DiscountManagement.Infrastructure
{
    public class DiscountContext:DbContext
    {
        public DbSet<CustomerDiscount> CustomerDiscounts { get; set; }
        public DbSet<ColleagueDiscount> ColleagueDiscounts { get; set; }

        public DiscountContext(DbContextOptions<DiscountContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerDiscountMapping());
            modelBuilder.ApplyConfiguration(new ColleagueDiscountMapping());
            base.OnModelCreating(modelBuilder);
        }
    }
}
