using Application.AuditsLog.DTOs;
using Application.CustomizedOrders.DTOs;
using Application.Interfaces.Common.Mappers;
using Application.ProductImages.DTOs;
using Domain.Entities;


namespace Application.ProductImages.Mapper;

public class ProductImageMapper : IEntityMapper<ProductImage, CreateProductImageRequest,
    UpdateProductImageRequest, ProductImageResponse>
{
    public ProductImage ToEntity(CreateProductImageRequest dto)
    {
        throw new NotImplementedException();
    }

  

    public ProductImageResponse ToResponse(ProductImage entity)
    {
        throw new NotImplementedException();
    }

 
    public void ToUpdateEntity(ProductImage entity, UpdateProductImageRequest dto)
    {
        throw new NotImplementedException();
    }
}