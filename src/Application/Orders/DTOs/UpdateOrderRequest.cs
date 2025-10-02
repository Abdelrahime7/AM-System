
using Domain.Enums;

namespace Application.Orders.DTOs;

public record UpdateOrderRequest
{
    public string? OrderRef { get; set; }

    public OrderType? OrderType { get; set; }

    public OrderStatus? Status { get; set; }

    public bool? IsCustomized { get; set; }

    public DateTime? ReviewedAt { get; set; }

    public DateTime? DepartedAt { get; set; }

    public DateTime? DeliveredAt { get; set; }

    public int? AffiliateId { get; set; }

    public int? CustomerId { get; set; }

    public int? DriverId { get; set; }

    public int? DeliveryCompanyId { get; set; }

    public int? ReviewedBy { get; set; }
}
