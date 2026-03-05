using Domain.Enums;

namespace Application.AuditsLog.DTOs;

public record UpdateAuditLogRequest
{
    public AuditAction? Action { get; set; }

    public string? TableName { get; set; }

    public int? RecordId { get; set; }

    public string? OldValues { get; set; }

    public string? NewValues { get; set; }

    public int? UserId { get; set; }
}