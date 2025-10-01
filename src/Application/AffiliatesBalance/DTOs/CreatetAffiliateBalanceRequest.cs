namespace Application.AffiliatesBalance.DTOs;

public record CreatetAuditLogRequest
{
    public required decimal Amount { get; set; }

    public required int AffiliateId { get; set; }

}