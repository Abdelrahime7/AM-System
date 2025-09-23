using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class WithdrawalConfiguration : IEntityTypeConfiguration<Withdrawal>
{
    public void Configure(EntityTypeBuilder<Withdrawal> builder)
    {
        builder.ToTable("withdrawals");

        builder.HasKey(w => w.Id);
        builder.Property(w => w.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(w => w.AffiliateId)
            .HasColumnName("affiliate_id")
            .IsRequired();

        builder.Property(w => w.Amount)
            .HasColumnName("amount")
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        builder.Property(w => w.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .HasDefaultValue(WithdrawalStatus.Pending);

        builder.Property(w => w.ProcessedBy)
            .HasColumnName("processed_by");

        builder.Property(w => w.ProcessedAt)
            .HasColumnName("processed_at")
            .HasColumnType("timestamp with time zone");
        
        builder.Property(w => w.AffiliateBalanceId)
            .HasColumnName("affiliate_balance_id")
            .IsRequired();
        
        //Relationships
        builder.HasOne(w => w.AffiliateBalance)
            .WithMany(ab => ab.Withdrawals)
            .HasForeignKey(w => w.AffiliateBalanceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}