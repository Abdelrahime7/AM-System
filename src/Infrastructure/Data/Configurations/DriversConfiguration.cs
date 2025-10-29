using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class DriversConfiguration : IEntityTypeConfiguration<Driver>
    {

      

        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.ToTable("Drivers");

            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(d => d.IsLocal)
                .HasColumnName("IsLocal")
                .IsRequired();

            builder.Property(d => d.IsAvailable)
               .HasColumnName("IsAvailable")
               .IsRequired();

            builder.Property(d => d.UserID)
              .HasColumnName("UserID")
              .IsRequired();


            //relationships
            builder.HasOne(d => d.User)
                .WithOne()
                .HasForeignKey<Driver>(d => d.UserID)
                .OnDelete(DeleteBehavior.Cascade); 



        }
    }
}
