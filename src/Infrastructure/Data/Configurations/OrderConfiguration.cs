using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders");

        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(o => o.OrderRef)
            .HasColumnName("order_ref")
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(o => o.OrderRef)
            .IsUnique();

        builder.Property(o => o.OrderType)
            .HasColumnName("order_type")
            .HasConversion<string>()
            .IsRequired();

        builder.Property(o => o.AffiliateId)
            .HasColumnName("affiliate_id")
            .IsRequired();

        builder.Property(o => o.CustomerId)
            .HasColumnName("customer_id")
            .IsRequired();

        builder.Property(o => o.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .IsRequired();

        builder.Property(o => o.IsCustomized)
            .HasColumnName("is_customized")
            .HasDefaultValue(false);

        builder.Property(o => o.DriverId)
            .HasColumnName("driver_id");

        builder.Property(o => o.DeliveryCompanyId)
            .HasColumnName("delivery_company_id");

        builder.Property(o => o.ReviewedBy)
            .HasColumnName("reviewed_by");

        builder.Property(o => o.ReviewedAt)
            .HasColumnName("reviewed_at")
            .HasColumnType("timestamp with time zone");

        builder.Property(o => o.DepartedAt)
            .HasColumnName("departed_at")
            .HasColumnType("timestamp with time zone");

        builder.Property(o => o.DeliveredAt)
            .HasColumnName("delivered_at")
            .HasColumnType("timestamp with time zone");

        // relationships
        builder.HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.Restrict); // or .Cascade if deletion should propagate

        builder.HasMany(o => o.OrderDetails)
            .WithOne(d => d.Order)
            .HasForeignKey(d => d.OrderId)
            .OnDelete(DeleteBehavior.Cascade); // or Restrict, depending on your domain rules

        builder.HasMany(o => o.Customizations)
            .WithOne(c => c.Order)
            .HasForeignKey(c => c.OrderId)
            .OnDelete(DeleteBehavior.Cascade); // or Restrict
    }
}