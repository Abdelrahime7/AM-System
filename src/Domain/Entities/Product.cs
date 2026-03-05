using Domain.Enums;

namespace Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public decimal CommissionAmount { get; set; }
    public ProductStatus Status { get; set; }
    public string? Dimensions { get; set; }
    public int TotalOrders { get; set; } = 0;
    public int CreatedByUserId { get; set; }

    // Navigation properties
    public User CreatedBy { get; set; } = null!;
    public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
}