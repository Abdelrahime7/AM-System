using Domain.Enums;

namespace Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public required string OrderRef { get; set; }
    public OrderType OrderType { get; set; }
    public OrderStatus Status { get; set; }
    public bool IsCustomized { get; set; } = false;
    public DateTime? ReviewedAt { get; set; }
    public DateTime? DepartedAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
    
    public int AffiliateId { get; set; }
    public User Affiliate { get; set; } = null!;
    
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    
    public int? DriverId { get; set; }
    public User? Driver { get; set; }

    public int? DeliveryCompanyId { get; set; }
    public DeliveryIntegration? DeliveryCompany { get; set; }

    public int? ReviewedBy { get; set; }
    public User? Reviewer { get; set; }
}