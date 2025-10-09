global using static Application.ProductImages.ImageValidationConstants;
using Application.ProductImages.DTOs;

namespace Application.ProductImages;

public static class ImageValidationConstants
{
    public static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
    public static readonly long MaxFileSizeInBytes = 5 * 1024 * 1024; // 5MB
    
    public static bool IsValidFileExtension(string? fileName)
    {
        if (string.IsNullOrEmpty(fileName))
            return false;

        var extension = Path.GetExtension(fileName).ToLowerInvariant();
        return AllowedExtensions.Contains(extension);
    }
    
    // XOR operation: true when one is set but not both
    public static bool HaveValidAssociation(int? productId, int? customizedOrderId) 
        => productId.HasValue ^ customizedOrderId.HasValue;
}
