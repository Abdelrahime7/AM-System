using Domain.Enums;

namespace Domain.Entities;

public class CustomizedOrder
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string Dimensions { get; set; }
    public CustomizedOrderStatus Status { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal CommissionAmount { get; set; }

    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;
    
    public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
}