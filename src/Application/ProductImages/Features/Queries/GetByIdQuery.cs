using Application.Common.Models;
using Application.ProductImages.DTOs;

namespace Application.ProductImages.Features.Queries;

public partial class ProductImageQueries
{
    public async Task<Result<ProductImageResponse>> GetByIdAsync(int id)
    {
        try
        {
            var productImage = await _repository.GetByIdAsync(id);
            if (productImage == null)
                return Result<ProductImageResponse>.Failure("Product image not found");

            var response = _mapper.ToResponse(productImage);
            return Result<ProductImageResponse>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<ProductImageResponse>.Failure($"Failed to fetch product image: {ex.Message}");
        }
    }
}