using Domain.Enums;

namespace Application.AuditsLog.DTOs;

public record CreateAuditLogRequest
{
    public AuditAction Action { get; set; }

    public required string  TableName { get; set; }

    public required int RecordId { get; set; }

    public required string OldValues { get; set; }

    public required string NewValues { get; set; }

    public required int UserId { get; set; }

}