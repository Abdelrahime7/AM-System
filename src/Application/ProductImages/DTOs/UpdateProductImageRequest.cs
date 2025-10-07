using Microsoft.AspNetCore.Http;

namespace Application.ProductImages.DTOs;

public class UpdateProductImageRequest
{
    public int Id { get; set; }
    public IFormFile? ImageFile { get; set; }
    public string? AltText { get; set; }
    public bool? IsPrimary { get; set; }
    public int? ProductId { get; set; }
    public int? CustomizedOrderId { get; set; }
}
