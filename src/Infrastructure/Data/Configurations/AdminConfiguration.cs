using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configurations
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {

            builder.ToTable("Admins");

            builder.HasKey(x => x.Id);

            builder. Property(d => d.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd(); ;

            builder.Property(x => x.access).
                HasColumnName("AccessLevel")
                .IsRequired();

          builder.HasOne(x=>x.user).
                WithOne().HasForeignKey<Admin>(x=>x.UserID)
                .OnDelete(DeleteBehavior.Cascade);



        }
    }
}
