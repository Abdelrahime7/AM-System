namespace Application.AffiliatesBalance.DTOs;

public record UpdateAffiliateBalanceRequest
{
    public int Id { get; set; }
    public decimal? Amount { get; set; }
}
