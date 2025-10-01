using Domain.Enums;

namespace Application.AffiliatesBalance.DTOs;

public record UpdateAffiliateBalanceRequest
{
    public decimal? Amount { get; set; }

    public int? AffiliateId { get; set; }
}