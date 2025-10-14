using Application.Common.Models;
using Application.ProductImages.DTOs;

namespace Application.ProductImages.Features.Queries;

public partial class ProductImageQueries
{
    public async Task<Result<ProductImageResponse>> GetPrimaryImageByCustomizedOrderIdAsync(int customizedOrderId)
    {
        try
        {
            var primaryImage = await _repository.GetPrimaryImageByCustomizedOrderIdAsync(customizedOrderId);
            if (primaryImage == null)
                return Result<ProductImageResponse>.Failure("No primary image found for this customized order");

            var response = _mapper.ToResponse(primaryImage);
            return Result<ProductImageResponse>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<ProductImageResponse>.Failure($"Failed to fetch primary image: {ex.Message}");
        }
    }
}