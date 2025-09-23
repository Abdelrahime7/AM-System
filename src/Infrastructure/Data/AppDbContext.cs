using System.Text.Json;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<CustomizedOrder> CustomizedOrders { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<AffiliateBalance> Earnings { get; set; }
    public DbSet<Withdrawal> Withdrawals { get; set; }
    public DbSet<DeliveryIntegration> DeliveryIntegrations { get; set; }
    public DbSet<CallLog> CallLogs { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }
    public DbSet<Token> Tokens { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
    
    /// <summary>
    /// Current user ID for audit logging. Must be set before calling SaveChanges to track who made the changes.
    /// </summary>
    public int? CurrentUserId { get; set; }

    /// <summary>
    /// Asynchronously saves changes to database with automatic timestamp updates and audit logging.
    /// Creates audit log entries for all Create/Update/Delete operations when CurrentUserId is set.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Number of entities affected</returns>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var auditEntries = CreateAuditEntries();
        if (auditEntries.Count > 0)
            await AuditLogs.AddRangeAsync(auditEntries, cancellationToken);

        return await base.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Synchronously saves changes to database with automatic timestamp updates and audit logging.
    /// Creates audit log entries for all Create/Update/Delete operations when CurrentUserId is set.
    /// </summary>
    /// <returns>Number of entities affected</returns>
    public override int SaveChanges()
    {
        var auditEntries = CreateAuditEntries();
        
        if (auditEntries.Count != 0)
            AuditLogs.AddRange(auditEntries);
        
        return base.SaveChanges();
    }
    
    /// <summary>
    /// Creates audit log entries for all tracked entity changes (Create/Update/Delete).
    /// Only creates entries when CurrentUserId is set. Excludes AuditLog entities to prevent recursion.
    /// </summary>
    /// <returns>List of audit log entries to be saved</returns>
    private List<AuditLog> CreateAuditEntries()
    {
        var auditEntries = new List<AuditLog>();
        
        // Skip if no current user (e.g., system operations)
        if (!CurrentUserId.HasValue)
            return auditEntries;
        
        var entries = ChangeTracker.Entries()
            .Where(e => e.State is EntityState.Added or EntityState.Modified or EntityState.Deleted)
            .Where(e => e.Entity is not AuditLog) // Don't audit the audit logs themselves
            .ToList();
        
        foreach (var entry in entries)
        {
            var auditLog = new AuditLog
            {
                UserId = CurrentUserId.Value,
                TableName = entry.Entity.GetType().Name,
                CreatedAt = DateTime.UtcNow
            };
            
            switch (entry.State)
            {
                case EntityState.Added:
                    auditLog.Action = AuditAction.Create;
                    auditLog.NewValues = JsonSerializer.Serialize(GetEntityValues(entry, AuditValueType.New));
                    break;
                    
                case EntityState.Modified:
                    auditLog.Action = AuditAction.Update;
                    auditLog.OldValues = JsonSerializer.Serialize(GetEntityValues(entry, AuditValueType.Old));
                    auditLog.NewValues = JsonSerializer.Serialize(GetEntityValues(entry, AuditValueType.New));
                    break;
                    
                case EntityState.Deleted:
                    auditLog.Action = AuditAction.Delete;
                    auditLog.OldValues = JsonSerializer.Serialize(GetEntityValues(entry, AuditValueType.Old));
                    break;
            }
            
            // Try to get the entity ID
            var idProperty = entry.Entity.GetType().GetProperty("Id");
            if (idProperty != null)
            {
                auditLog.RecordId = (int?)idProperty.GetValue(entry.Entity);
            }
            
            auditEntries.Add(auditLog);
        }
        
        return auditEntries;
    }
    
    /// <summary>
    /// Extracts property values from an entity entry for audit logging,
    /// returning either the original (old) or current (new) values.
    /// </summary>
    /// <param name="entry">The tracked entity entry to extract values from.</param>
    /// <param name="valueType">Specifies whether to retrieve old or new values.</param>
    /// <returns>A dictionary mapping property names to their extracted values.</returns>
    private static Dictionary<string, object?> GetEntityValues(EntityEntry entry, AuditValueType valueType)
    {
        var values = new Dictionary<string, object?>();

        foreach (var property in entry.Properties)
        {
            values[property.Metadata.Name] = valueType switch
            {
                AuditValueType.New => property.CurrentValue,
                AuditValueType.Old => property.OriginalValue,
                _ => throw new ArgumentOutOfRangeException(nameof(valueType), valueType, null)
            };
        }

        return values;
    }
}