using Application.Common.Models;

namespace Application.Products.Features.Commands;

public partial class ProductCommands
{
    public async Task<Result<bool>> DeleteProductAsync(int id)
    {
        try
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
                return Result<bool>.Failure("Product Not Found");
            
            _repository.Delete(product);
            return Result<bool>.Success(true);
        }
        catch (Exception e)
        {
            return Result<bool>.Failure($"Error updating product: {e.Message}");
        }
    }
}