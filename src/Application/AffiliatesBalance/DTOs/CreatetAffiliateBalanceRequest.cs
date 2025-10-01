namespace Application.AffiliatesBalance.DTOs;

public record CreatetAffiliateBalanceRequest
{
 
        public decimal Amount { get; set; }

        public int AffiliateId { get; set; }
    


}