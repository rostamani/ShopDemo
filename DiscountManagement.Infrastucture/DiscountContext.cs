using System;
using System.Collections.Generic;
using System.Text;
using DiscountManagement.Domain.CustomerDiscountAgg;
using Microsoft.EntityFrameworkCore;

namespace DiscountManagement.Infrastructure
{
    public class DiscountContext:DbContext
    {
        public DbSet<CustomerDiscount> CustomerDiscounts { get; set; }

        public DiscountContext(DbContextOptions<DiscountContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
