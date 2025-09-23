using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class CallLogConfiguration : IEntityTypeConfiguration<CallLog>
{
    public void Configure(EntityTypeBuilder<CallLog> builder)
    {
        builder.ToTable("call_logs");

        builder.HasKey(cl => cl.Id);
        builder.Property(cl => cl.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(cl => cl.OrderId)
            .HasColumnName("order_id")
            .IsRequired();

        builder.Property(cl => cl.AgentId)
            .HasColumnName("agent_id")
            .IsRequired();

        builder.Property(cl => cl.CustomerPhone)
            .HasColumnName("customer_phone")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(cl => cl.CallResult)
            .HasColumnName("call_result")
            .HasConversion<string>();

        builder.Property(cl => cl.CalledAt)
            .HasColumnName("called_at")
            .HasColumnType("timestamp with time zone")
            .IsRequired();
    }
}