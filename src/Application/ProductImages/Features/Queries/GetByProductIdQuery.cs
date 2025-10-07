using Application.Common.Models;
using Application.ProductImages.DTOs;

namespace Application.ProductImages.Features.Queries;

public partial class ProductImageQueries
{
    public async Task<Result<IEnumerable<ProductImageResponse>>> GetByProductIdAsync(int productId)
    {
        try
        {
            var productImages = await _repository.GetByProductIdAsync(productId);
            if (!productImages.Any())
                return Result<IEnumerable<ProductImageResponse>>.Failure("No product images found for this product");

            var response = productImages.Select(image => _mapper.ToResponse(image));
            return Result<IEnumerable<ProductImageResponse>>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<ProductImageResponse>>.Failure($"Failed to fetch product images: {ex.Message}");
        }
    }
}