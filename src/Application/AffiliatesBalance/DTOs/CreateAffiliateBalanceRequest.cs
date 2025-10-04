namespace Application.AffiliatesBalance.DTOs;

public record CreateAffiliateBalanceRequest
{
        public decimal Amount { get; set; }
        public int AffiliateId { get; set; }
}