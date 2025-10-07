using Application.Common.Models;
using Application.ProductImages.DTOs;

namespace Application.ProductImages.Features.Queries;

public partial class ProductImageQueries
{
    public async Task<Result<ProductImageResponse>> GetPrimaryImageByProductIdAsync(int productId)
    {
        try
        {
            var primaryImage = await _repository.GetPrimaryImageByProductIdAsync(productId);
            if (primaryImage == null)
                return Result<ProductImageResponse>.Failure("No primary image found for this product");

            var response = _mapper.ToResponse(primaryImage);
            return Result<ProductImageResponse>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<ProductImageResponse>.Failure($"Failed to fetch primary image: {ex.Message}");
        }
    }
}