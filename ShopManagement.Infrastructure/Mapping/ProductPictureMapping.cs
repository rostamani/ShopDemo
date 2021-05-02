using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Infrastructure.Mapping
{
    public class ProductPictureMapping:IEntityTypeConfiguration<ProductPicture>
    {
        public void Configure(EntityTypeBuilder<ProductPicture> builder)
        {
            builder.ToTable("ProductPictures");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Picture).HasMaxLength(1000).IsRequired();
            builder.Property(p => p.PictureAlt).HasMaxLength(500).IsRequired();
            builder.Property(p => p.PictureTitle).HasMaxLength(500).IsRequired();


            builder.HasOne(p => p.Product).WithMany(p => p.ProductPictures).HasForeignKey(p => p.ProductId);
        }
    }
}
