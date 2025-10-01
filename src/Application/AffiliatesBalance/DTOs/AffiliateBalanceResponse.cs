
namespace Application.AffiliateBalance.DTOs;

public record AuditLogResponse
{
    public decimal Amount { get; set; }
    public int AffiliateId { get; set; }
   

}