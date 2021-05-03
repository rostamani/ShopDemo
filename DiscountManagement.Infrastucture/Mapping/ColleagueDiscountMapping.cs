using System;
using System.Collections.Generic;
using System.Text;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscountManagement.Infrastructure.Mapping
{
    class ColleagueDiscountMapping:IEntityTypeConfiguration<ColleagueDiscount>
    {
        public void Configure(EntityTypeBuilder<ColleagueDiscount> builder)
        {
            builder.ToTable("ColleagueDiscounts");
            builder.HasKey(cd => cd.Id);
        }
    }
}
