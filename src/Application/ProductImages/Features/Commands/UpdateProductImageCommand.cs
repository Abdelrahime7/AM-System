using Application.Common.Models;
using Application.ProductImages.DTOs;

namespace Application.ProductImages.Features.Commands;

public partial class ProductImageCommands
{
    public async Task<Result<bool>> UpdateProductImageAsync(UpdateProductImageRequest request)
    {
        try
        {
            var productImage = await _repository.GetByIdAsync(request.Id);
            if (productImage == null)
                return Result<bool>.Failure("Product image not found");

            if (request.ImageFile != null && request.ImageFile.Length > 0)
            {
                var oldImageUrl = productImage.ImageUrl;
                await using (var stream = request.ImageFile.OpenReadStream())
                {
                    var fileExtension = Path.GetExtension(request.ImageFile.FileName).ToLowerInvariant();
                    var fileName = $"{Guid.NewGuid()}{fileExtension}";
                    productImage.ImageUrl = await _fileStorageService.UploadFileAsync(stream, fileName, request.ImageFile.ContentType);
                }

                await _fileStorageService.DeleteFileAsync(oldImageUrl);
            }
            
            
            // Handle primary image setting
            if (request.IsPrimary.HasValue && request.IsPrimary.Value)
            {
                if (productImage.ProductId.HasValue)
                    await ResetPrimaryImagesForProductAsync(productImage.ProductId.Value);
                else if (productImage.CustomizedOrderId.HasValue)
                    await ResetPrimaryImagesForCustomizedOrderAsync(productImage.CustomizedOrderId.Value);
            }

            _mapper.ToUpdateEntity(productImage, request);
            _repository.Update(productImage);
            return Result<bool>.Success(true);
        }
        catch (Exception e)
        {
            return Result<bool>.Failure($"Failed to update product image: {e.Message}");
        }
    }
}