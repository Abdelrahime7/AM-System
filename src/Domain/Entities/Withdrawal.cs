using Domain.Enums;

namespace Domain.Entities;

public class Withdrawal
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public WithdrawalStatus Status { get; set; }
    public DateTime? ProcessedAt { get; set; }

    // Navigation properties
    public int AffiliateId { get; set; }
    public User Affiliate { get; set; } = null!;
    
    public int? ProcessedBy { get; set; }
    public User? ProcessedByUser { get; set; }
    
    public int AffiliateBalanceId { get; set; }
    public AffiliateBalance AffiliateBalance { get; set; } = null!;
}