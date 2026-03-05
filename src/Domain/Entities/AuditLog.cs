using Domain.Enums;

namespace Domain.Entities;

public class AuditLog
{
    public int Id { get; set; }
    public AuditAction Action { get; set; }
    public string? TableName { get; set; }
    public int? RecordId { get; set; }
    public string? OldValues { get; set; } 
    public string? NewValues { get; set; } 
    public DateTime CreatedAt { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;
}