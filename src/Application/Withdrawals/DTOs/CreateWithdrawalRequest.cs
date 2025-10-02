using Domain.Enums;

namespace Application.Withdrawals.DTOs;


public record CreateWithdrawalRequest
{
    public decimal Amount { get; set; }

    public WithdrawalStatus Status { get; set; }

    public int AffiliateId { get; set; }

    public int AffiliateBalanceId { get; set; }

    public int? ProcessedBy { get; set; }

    public DateTime? ProcessedAt { get; set; }
}



