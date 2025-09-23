using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class DeliveryIntegrationConfiguration : IEntityTypeConfiguration<DeliveryIntegration>
{
    public void Configure(EntityTypeBuilder<DeliveryIntegration> builder)
    {
        builder.ToTable("delivery_integrations");

        builder.HasKey(di => di.Id);
        builder.Property(di => di.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(di => di.Name)
            .HasColumnName("name")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(di => di.ApiEndpoint)
            .HasColumnName("api_endpoint")
            .HasMaxLength(255);

        builder.Property(di => di.ApiKey)
            .HasColumnName("api_key")
            .HasMaxLength(255);

        builder.Property(di => di.ApiSecret)
            .HasColumnName("api_secret")
            .HasMaxLength(255);

        builder.Property(di => di.IsActive)
            .HasColumnName("is_active")
            .HasDefaultValue(true);
    }
}