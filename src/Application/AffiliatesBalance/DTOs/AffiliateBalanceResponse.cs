namespace Application.AffiliatesBalance.DTOs;

public record AffiliateBalanceResponse
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public int AffiliateId { get; set; }
    public string? AffiliateName { get; set; }
}