using System;
using System.Collections.Generic;
using System.Text;
using InventoryManagement.Domain.InventoryAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Infrastructure.Mapping
{
    public class InventoryMapping:IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.ToTable("Inventory");
            builder.HasKey(i => i.Id);
            builder.OwnsMany(i => i.Operations, modelBuilder =>
            {
                modelBuilder.ToTable("InventoryOperations");
                modelBuilder.HasKey(o => o.Id);
                modelBuilder.Property(o => o.Description).HasMaxLength(1000);
                modelBuilder.WithOwner(o => o.Inventory).HasForeignKey(i => i.InventoryId);
            });
        }
    }
}
