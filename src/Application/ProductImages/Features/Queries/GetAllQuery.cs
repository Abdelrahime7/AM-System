using Application.Common.Models;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.ProductImagesInterfaces;
using Application.Interfaces.Repositories;
using Application.ProductImages.DTOs;
using Domain.Entities;

namespace Application.ProductImages.Features.Queries;

public partial class ProductImageQueries(
    IProductImageRepository repository,
    IEntityMapper<ProductImage, CreateProductImageRequest, UpdateProductImageRequest, ProductImageResponse> mapper)
    : IProductImageQueries
{
    private readonly IProductImageRepository _repository = repository;
    private readonly IEntityMapper<ProductImage, CreateProductImageRequest, UpdateProductImageRequest, ProductImageResponse> _mapper = mapper;

    public async Task<Result<IEnumerable<ProductImageResponse>>> GetAllAsync()
    {
        try
        {
            var productImages = await _repository.GetAllAsync();
            if (!productImages.Any())
                return Result<IEnumerable<ProductImageResponse>>.Failure("No product images found");

            var response = productImages.Select(image => _mapper.ToResponse(image));
            return Result<IEnumerable<ProductImageResponse>>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<ProductImageResponse>>.Failure($"Failed to fetch product images: {ex.Message}");
        }
    }
}