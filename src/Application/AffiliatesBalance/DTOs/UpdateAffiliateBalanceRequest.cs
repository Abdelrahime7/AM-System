using Domain.Enums;

namespace Application.AffiliateBalance.DTOs;

public record UpdateAuditLogRequest
{
    public decimal? Amount { get; set; }
    public int? AffiliateId { get; set; }
}