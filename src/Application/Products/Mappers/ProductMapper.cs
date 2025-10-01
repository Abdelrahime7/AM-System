using Application.Interfaces.Common.Mappers;
using Application.Products.DTOs;
using Domain.Entities;
using Domain.Enums;

namespace Application.Products.Mappers
{
    public class ProductMapper : IEntityMapper<Product, CreatetAffiliateBalanceRequest, UpdateAffiliateBalanceRequest, AffiliateBalanceResponse>
    {
        public Product ToEntity(CreatetAffiliateBalanceRequest dto)
        {
            return new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                CommissionAmount = dto.CommissionAmount,
                Status = dto.Status,
                Dimensions = dto.Dimensions,
                CreatedByUserId = dto.CreatedByUserId
            };
        }

        public AffiliateBalanceResponse ToResponse(Product entity)
        {
            return new AffiliateBalanceResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Price = entity.Price,
                CommissionAmount = entity.CommissionAmount,
                Status = entity.Status,
                Dimensions = entity.Dimensions,
                TotalOrders = entity.TotalOrders,
                CreatedByUserId = entity.CreatedByUserId,
                CreatedByName = entity.CreatedBy?.FullName,
                /* Refactor Later
                 Images = entity.Images.Select(img => new ProductImageResponse
                {
                    Id = img.Id,
                    Url = img.Url
                }).ToList()
                */
            };
        }
        
        public void ToUpdateEntity(Product entity, UpdateAffiliateBalanceRequest dto)
        {
            entity.Name = dto.Name ?? entity.Name;
            entity.Description = dto.Description ?? entity.Description;
            entity.Price = dto.Price ?? entity.Price;
            entity.CommissionAmount =  dto.CommissionAmount ?? entity.CommissionAmount;
            entity.Status = dto.Status ?? entity.Status;
            entity.Dimensions = dto.Dimensions ?? entity.Dimensions;
            entity.CreatedByUserId = dto.CreatedByUserId ?? entity.CreatedByUserId;
            
            if (dto.Status.HasValue && Enum.IsDefined(typeof(ProductStatus), dto.Status.Value))
            {
                entity.Status = (ProductStatus)dto.Status.Value;
            }
        }
    }
}
