using Domain.Enums;

namespace Application.Products.DTOs;

public record CreatetAffiliateBalanceRequest
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public decimal CommissionAmount { get; set; }
    public ProductStatus Status { get; set; }
    public string? Dimensions { get; set; }
    public int CreatedByUserId { get; set; }
}