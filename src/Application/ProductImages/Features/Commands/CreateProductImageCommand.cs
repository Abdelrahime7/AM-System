using Application.Common.Models;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.ProductImagesInterfaces;
using Application.Interfaces.Repositories;
using Application.ProductImages.DTOs;
using Domain.Entities;

namespace Application.ProductImages.Features.Commands;

public partial class ProductImageCommands(
    IProductImageRepository repository,
    IFileStorageService fileStorageService,
    IEntityMapper<ProductImage, CreateProductImageRequest, UpdateProductImageRequest, ProductImageResponse> mapper)
    : IProductImageCommands
{
    private readonly IProductImageRepository _repository = repository;
    private readonly IFileStorageService _fileStorageService = fileStorageService;
    private readonly IEntityMapper<ProductImage, CreateProductImageRequest, UpdateProductImageRequest, ProductImageResponse> _mapper = mapper;

    public async Task<Result<int>> CreateProductImageAsync(CreateProductImageRequest request)
    {
        try
        {
            string imageUrl;
            await using (var stream = request.ImageFile.OpenReadStream())
            {
                var fileExtension = Path.GetExtension(request.ImageFile.FileName).ToLowerInvariant();
                var fileName = $"{Guid.NewGuid()}{fileExtension}";
                imageUrl = await _fileStorageService.UploadFileAsync(stream, fileName, request.ImageFile.ContentType);
            }
            
            // If setting as primary, ensure only one primary image exists
            if (request.IsPrimary)
            {
                if (request.ProductId.HasValue)
                {
                    await ResetPrimaryImagesForProductAsync(request.ProductId.Value);
                }
                else if (request.CustomizedOrderId.HasValue)
                {
                    await ResetPrimaryImagesForCustomizedOrderAsync(request.CustomizedOrderId.Value);
                }
            }
            
            var productImage = _mapper.ToEntity(request);
            productImage.ImageUrl = imageUrl;
            await _repository.AddAsync(productImage);
            return Result<int>.Success(productImage.Id);
        }
        catch (Exception e)
        {
            return Result<int>.Failure($"Error creating product image: {e.Message}");
        }
    }

    private async Task ResetPrimaryImagesForProductAsync(int productId)
    {
        var existingPrimaryImages = await _repository.GetByProductIdAsync(productId);
        foreach (var image in existingPrimaryImages.Where(img => img.IsPrimary))
        {
            image.IsPrimary = false;
            _repository.Update(image);
        }
    }

    private async Task ResetPrimaryImagesForCustomizedOrderAsync(int customizedOrderId)
    {
        var existingPrimaryImages = await _repository.GetByCustomizedOrderIdAsync(customizedOrderId);
        foreach (var image in existingPrimaryImages.Where(img => img.IsPrimary))
        {
            image.IsPrimary = false;
            _repository.Update(image);
        }
    }
}