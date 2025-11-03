using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Data.Configurations
{
    public class AffiliateConfiguration : IEntityTypeConfiguration<Affiliate>
    {
        public void Configure(EntityTypeBuilder<Affiliate> builder)
        {

            builder.ToTable("Affiliates");

            builder.HasKey(x => x.Id);

            builder. Property(d => d.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd(); ;

            builder.Property(x => x.UserID).
                HasColumnName("User_id")
                .IsRequired();

            builder.Property(x => x.ReferralCode).
                HasColumnName("ReferralCode");

            builder.Property(x => x.CommissionRate).
               HasColumnName("Commission_Rate");


            builder.Property(x => x.PartnerSince).
               HasColumnName("Partner_Since");


            builder.HasOne(x=>x.user).
                WithOne().HasForeignKey<Affiliate>(x=>x.UserID)
                .OnDelete(DeleteBehavior.Cascade);



        }
    }
}
