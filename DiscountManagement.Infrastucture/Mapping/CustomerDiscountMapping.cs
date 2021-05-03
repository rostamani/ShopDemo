using System;
using System.Collections.Generic;
using System.Text;
using DiscountManagement.Domain.CustomerDiscountAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscountManagement.Infrastructure.Mapping
{
    public class CustomerDiscountMapping:IEntityTypeConfiguration<CustomerDiscount>
    {

        public void Configure(EntityTypeBuilder<CustomerDiscount> builder)
        {
            builder.ToTable("CustomerDiscounts");
            builder.HasKey(cd => cd.Id);
            builder.Property(cd => cd.Reason).HasMaxLength(500);
        }
    }
}
