using System;
using System.Collections.Generic;
using System.Text;
using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure
{
    public class InventoryContext:DbContext
    {
        public DbSet<Inventory> Inventory { get; set; }

        public InventoryContext(DbContextOptions<InventoryContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new InventoryMapping());
            base.OnModelCreating(modelBuilder);
        }
    }
}
