using Application.Common.Models;
using Application.Products.DTOs;

namespace Application.Products.Features.Commands;

public partial class ProductCommands
{
    public async Task<Result<bool>> UpdateProductAsync(UpdateAffiliateBalanceRequest request)
    {
        try
        {
            var product = await _repository.GetByIdAsync(request.Id);
            if (product == null)
                return Result<bool>.Failure("Product not found");
            
            _mapper.ToUpdateEntity(product, request);
            _repository.Update(product);
            return Result<bool>.Success(true);
        }
        catch (Exception e)
        {
            return Result<bool>.Failure($"Failed to update product: {e.Message}");
        }
    }
}