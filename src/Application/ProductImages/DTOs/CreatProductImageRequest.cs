using Domain.Enums;

namespace Application.ProductImages.DTOs;

public class CreateProductImageRequest
{
    public string ImageUrl { get; set; } = string.Empty;

    public string? AltText { get; set; }

    public bool IsPrimary { get; set; }

    public int? ProductId { get; set; }

    public int? CustomizedOrderId { get; set; }
}
