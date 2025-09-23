using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class EarningConfiguration : IEntityTypeConfiguration<AffiliateBalance>
{
    public void Configure(EntityTypeBuilder<AffiliateBalance> builder)
    {
        builder.ToTable("affiliate_balance");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(e => e.AffiliateId)
            .HasColumnName("affiliate_id")
            .IsRequired();

        builder.Property(e => e.Amount)
            .HasColumnName("amount")
            .HasColumnType("decimal(12,2)")
            .IsRequired();

        // Relationships
        builder.HasMany(e => e.Withdrawals)
            .WithOne(w => w.AffiliateBalance)
            .HasForeignKey(w => w.AffiliateBalanceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}