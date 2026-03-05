using Microsoft.AspNetCore.Http;

namespace Application.ProductImages.DTOs;

public class CreateProductImageRequest
{
    public IFormFile ImageFile { get; set; } = null!;
    public string? AltText { get; set; }
    public bool IsPrimary { get; set; }
    public int? ProductId { get; set; }
    public int? CustomizedOrderId { get; set; }
}
