
namespace Application.Withdrawals.DTOs;


public record WithdrawalResponse
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime? ProcessedAt { get; set; }
    public int AffiliateId { get; set; }
    public string AffiliateName { get; set; } = string.Empty;
    public int AffiliateBalanceId { get; set; }
    public decimal CurrentBalance { get; set; }
    public int? ProcessedBy { get; set; }
    public string? ProcessedByName { get; set; }
}






