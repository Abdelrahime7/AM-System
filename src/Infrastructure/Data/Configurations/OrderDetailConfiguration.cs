using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.ToTable("order_details");

        builder.HasKey(od => od.Id);
        builder.Property(od => od.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(od => od.OrderId)
            .HasColumnName("order_id")
            .IsRequired();

        builder.Property(od => od.ProductId)
            .HasColumnName("product_id")
            .IsRequired();

        builder.Property(od => od.Quantity)
            .HasColumnName("quantity");

        builder.Property(od => od.UnitPrice)
            .HasColumnName("unit_price")
            .HasColumnType("decimal(10,2)");

        builder.Property(od => od.UnitCommission)
            .HasColumnName("unit_commission")
            .HasColumnType("decimal(8,2)");

        builder.Property(od => od.TotalPrice)
            .HasColumnName("total_price")
            .HasColumnType("decimal(10,2)");

        builder.Property(od => od.TotalCommission)
            .HasColumnName("total_commission")
            .HasColumnType("decimal(8,2)");

        //relationships
        builder.HasOne(d => d.Order).WithMany(o => o.OrderDetails)
            .HasForeignKey(d => d.OrderId).OnDelete(DeleteBehavior.Cascade);

    }
}