using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class CustomizedOrderConfiguration : IEntityTypeConfiguration<CustomizedOrder>
{
    public void Configure(EntityTypeBuilder<CustomizedOrder> builder)
    {
        builder.ToTable("customized_orders");

        builder.HasKey(co => co.Id);
        builder.Property(co => co.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(co => co.OrderId)
            .HasColumnName("order_id")
            .IsRequired();

        builder.Property(co => co.Name)
            .HasColumnName("name")
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(co => co.Description)
            .HasColumnName("description")
            .HasMaxLength(500);

        builder.Property(co => co.Dimensions)
            .HasColumnName("dimensions")
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(co => co.Status)
            .HasColumnName("status")
            .HasConversion<string>();

        builder.Property(co => co.TotalPrice)
            .HasColumnName("total_price")
            .HasColumnType("decimal(8,2)")
            .IsRequired();

        builder.Property(co => co.CommissionAmount)
            .HasColumnName("commission_amount")
            .HasColumnType("decimal(8,2)")
            .IsRequired();

        // Relationships
        builder.HasMany(co => co.Images)
            .WithOne(i => i.CustomizedOrder)
            .HasForeignKey(i => i.CustomizedOrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}