using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class AssisstantConfiguration : IEntityTypeConfiguration<Assisstant>
    {
        public void Configure(EntityTypeBuilder<Assisstant> builder)
        {
            builder.ToTable("Assisstants");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.AssignedBy).HasColumnName("Assigned_By");
            builder.Property(x => x.UserId).HasColumnName("User_Id");
            builder.Property(x => x.AssignedBy).HasColumnName("Assigned_By");

            builder.HasOne(x => x.User).WithOne().
                HasForeignKey<Assisstant>(x => x.UserId);

        }
    }
}
