using Application.Common.Models;
using Application.ProductImages.DTOs;

namespace Application.ProductImages.Features.Queries;

public partial class ProductImageQueries
{
    public async Task<Result<IEnumerable<ProductImageResponse>>> GetByCustomizedOrderIdAsync(int customizedOrderId)
    {
        try
        {
            var productImages = await _repository.GetByCustomizedOrderIdAsync(customizedOrderId);
            if (!productImages.Any())
                return Result<IEnumerable<ProductImageResponse>>.Failure("No product images found for this customized order");

            var response = productImages.Select(image => _mapper.ToResponse(image));
            return Result<IEnumerable<ProductImageResponse>>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<ProductImageResponse>>.Failure($"Failed to fetch product images: {ex.Message}");
        }
    }
}