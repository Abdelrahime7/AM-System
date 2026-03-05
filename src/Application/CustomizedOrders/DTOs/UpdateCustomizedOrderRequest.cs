using Domain.Enums;

namespace Application.CustomizedOrders.DTOs;

public record UpdateCustomizedOrderRequest
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Dimensions { get; set; }

    public CustomizedOrderStatus? Status { get; set; }

    public decimal? TotalPrice { get; set; }

    public decimal? CommissionAmount { get; set; }

    public int? OrderId { get; set; }

    public List<string>? ImageUrls { get; set; }
}