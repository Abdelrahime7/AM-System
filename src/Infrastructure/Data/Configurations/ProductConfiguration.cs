using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(p => p.Name)
            .HasColumnName("name")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasColumnName("description")
            .HasColumnType("text");

        builder.Property(p => p.Price)
            .HasColumnName("price")
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        builder.Property(p => p.CommissionAmount)
            .HasColumnName("commission_amount")
            .HasColumnType("decimal(8,2)")
            .IsRequired();

        builder.Property(p => p.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .HasDefaultValue(ProductStatus.Active);

        builder.Property(p => p.Dimensions)
            .HasColumnName("dimensions")
            .HasMaxLength(50);

        builder.Property(p => p.TotalOrders)
            .HasColumnName("total_orders")
            .HasDefaultValue(0);

        builder.Property(p => p.CreatedByUserId)
            .HasColumnName("created_by_user_id")
            .IsRequired();
        
        // Relationships
        builder.HasOne(p => p.CreatedBy)
            .WithMany() 
            .HasForeignKey(p => p.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(p => p.Images)
            .WithOne(i => i.Product)
            .HasForeignKey(i => i.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}