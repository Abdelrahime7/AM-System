using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.ToTable("audit_logs");

        builder.HasKey(al => al.Id);
        builder.Property(al => al.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(al => al.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.Property(al => al.Action)
            .HasColumnName("action")
            .HasConversion<string>()
            .IsRequired();

        builder.Property(al => al.TableName)
            .HasColumnName("table_name")
            .HasMaxLength(50);

        builder.Property(al => al.RecordId)
            .HasColumnName("record_id");

        builder.Property(al => al.OldValues)
            .HasColumnName("old_values")
            .HasColumnType("jsonb");

        builder.Property(al => al.NewValues)
            .HasColumnName("new_values")
            .HasColumnType("jsonb");

        builder.Property(al => al.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}