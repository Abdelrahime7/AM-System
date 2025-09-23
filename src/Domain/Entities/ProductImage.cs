namespace Domain.Entities;

public class ProductImage
{
    public int Id { get; set; }
    public required string ImageUrl { get; set; }
    public string? AltText { get; set; }
    public bool IsPrimary { get; set; }

    public int? ProductId { get; set; }
    public Product? Product { get; set; }
    
    public int? CustomizedOrderId { get; set; }
    public CustomizedOrder? CustomizedOrder { get; set; }
}