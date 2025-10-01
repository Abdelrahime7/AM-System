using Domain.Entities;
using Domain.Enums;

namespace Application.Products.DTOs;

public record ProductResponse
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public decimal CommissionAmount { get; set; }
    public ProductStatus Status { get; set; }
    public string? Dimensions { get; set; }
    public int TotalOrders { get; set; }
    public int CreatedByUserId { get; set; }
    public string? CreatedByName { get; set; }
    
    //Refactor later
    // public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
}