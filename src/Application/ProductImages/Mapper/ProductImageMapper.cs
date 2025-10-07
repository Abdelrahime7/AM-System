using Application.Interfaces.Common.Mappers;
using Application.ProductImages.DTOs;
using Domain.Entities;

namespace Application.ProductImages.Mapper;

public class ProductImageMapper : IEntityMapper<ProductImage, CreateProductImageRequest, UpdateProductImageRequest, ProductImageResponse>
{
    public ProductImage ToEntity(CreateProductImageRequest dto)
    {
        return new ProductImage
        {
            ImageUrl = string.Empty,
            AltText = dto.AltText,
            IsPrimary = dto.IsPrimary,
            ProductId = dto.ProductId,
            CustomizedOrderId = dto.CustomizedOrderId
        };
    }

    public ProductImageResponse ToResponse(ProductImage entity)
    {
        return new ProductImageResponse
        {
            Id = entity.Id,
            ImageUrl = entity.ImageUrl,
            AltText = entity.AltText,
            IsPrimary = entity.IsPrimary,
            ProductId = entity.ProductId,
            ProductName = entity.Product?.Name,
            CustomizedOrderId = entity.CustomizedOrderId,
            CustomizedOrderName = entity.CustomizedOrder?.Name 
        };
    }

    public void ToUpdateEntity(ProductImage entity, UpdateProductImageRequest dto)
    {
        entity.AltText = dto.AltText ?? entity.AltText;
        
        if (dto.IsPrimary.HasValue)
        {
            entity.IsPrimary = dto.IsPrimary.Value;
        }
        
        if (dto.ProductId.HasValue)
        {
            entity.ProductId = dto.ProductId.Value;
        }
        
        if (dto.CustomizedOrderId.HasValue)
        {
            entity.CustomizedOrderId = dto.CustomizedOrderId.Value;
        }
    }
}