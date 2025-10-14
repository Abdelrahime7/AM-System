using Application.Common.Models;

namespace Application.ProductImages.Features.Commands;

public partial class ProductImageCommands
{
    public async Task<Result<bool>> DeleteProductImageAsync(int id)
    {
        try
        {
            var productImage = await _repository.GetByIdAsync(id);
            if (productImage == null)
                return Result<bool>.Failure("Product image not found");

            await _fileStorageService.DeleteFileAsync(productImage.ImageUrl);
            _repository.Delete(productImage);
            return Result<bool>.Success(true);
        }
        catch (Exception e)
        {
            return Result<bool>.Failure($"Error deleting product image: {e.Message}");
        }
    }
}