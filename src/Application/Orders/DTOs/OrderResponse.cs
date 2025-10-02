namespace Application.Orders.DTOs;

public record OrderResponse
{
    public int Id { get; set; }

    public string OrderRef { get; set; } = string.Empty;

    public string OrderType { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public bool IsCustomized { get; set; }

    public DateTime? ReviewedAt { get; set; }

    public DateTime? DepartedAt { get; set; }

    public DateTime? DeliveredAt { get; set; }

    public int AffiliateId { get; set; }

    public string AffiliateName { get; set; } = string.Empty;

    public int CustomerId { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public int? DriverId { get; set; }

    public string? DriverName { get; set; }

    public int? DeliveryCompanyId { get; set; }

    public string? DeliveryCompanyName { get; set; }

    public int? ReviewedBy { get; set; }

    public string? ReviewerName { get; set; }
}




