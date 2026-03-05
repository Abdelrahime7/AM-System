namespace Application.ProductImages.DTOs;

public class ProductImageResponse
{
    public int Id { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    public string? AltText { get; set; }

    public bool IsPrimary { get; set; }

    public int? ProductId { get; set; }

    public string? ProductName { get; set; }

    public int? CustomizedOrderId { get; set; }

    public string? CustomizedOrderName { get; set; }
}
