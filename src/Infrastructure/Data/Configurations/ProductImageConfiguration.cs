using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.ToTable("product_images");

        builder.HasKey(pi => pi.Id);
        builder.Property(pi => pi.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(pi => pi.ProductId)
            .HasColumnName("product_id");

        builder.Property(pi => pi.CustomizedOrderId)
            .HasColumnName("customized_order_id");

        builder.Property(pi => pi.ImageUrl)
            .HasColumnName("image_url")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(pi => pi.AltText)
            .HasColumnName("alt_text")
            .HasMaxLength(120);

        builder.Property(pi => pi.IsPrimary)
            .HasColumnName("is_primary")
            .HasDefaultValue(false);
    }
}