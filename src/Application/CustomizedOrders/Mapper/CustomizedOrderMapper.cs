using Application.AuditsLog.DTOs;
using Application.CustomizedOrders.DTOs;
using Application.Interfaces.Common.Mappers;
using Domain.Entities;


namespace Application.CustomizedOrders.Mapper;

public class CustomizedOrderMapper : IEntityMapper<CustomizedOrder, CreateCustomizedOrderRequest,
    UpdateCustomizedOrderRequest, CustomizedOrderResponse>
{
    public CustomizedOrder ToEntity(CreateCustomizedOrderRequest dto)
    {

        return new CustomizedOrder
        {
            Name = dto.Name,
            Description = dto.Description, // Nullable in entity, so no issue
            Dimensions = dto.Dimensions,
            Status = dto.Status,
            TotalPrice = 1000m,
         CommissionAmount = 10m,
       //  Order =dto.Order,
            Images = dto.ImageUrls
                .Where(url => !string.IsNullOrWhiteSpace(url))
                .Select(url => new ProductImage { ImageUrl = url })
                .ToList()
        };
    

    }

    public CustomizedOrderResponse ToResponse(CustomizedOrder entity)
    {
       
        return new CustomizedOrderResponse
        {
            id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            Dimensions = entity.Dimensions,
            Status = entity.Status.ToString(), // Converts enum to string
            TotalPrice = entity.TotalPrice,
            CommissionAmount = entity.CommissionAmount,
            OrderId = entity.OrderId,
            OrderReference = entity.Order?.OrderRef?? string.Empty, // Defensive null check
            ImageUrls = entity.Images?
                .Where(img => !string.IsNullOrWhiteSpace(img.ImageUrl))
                .Select(img => img.ImageUrl)
                .ToList() ?? new List<string>()
        };
    

    }

    public void ToUpdateEntity(CustomizedOrder entity, UpdateCustomizedOrderRequest dto)
    {
        
        entity.Name = dto.Name ?? entity.Name;
        entity.Description = dto.Description ?? entity.Description;
        entity.Dimensions = dto.Dimensions ?? entity.Dimensions;
        entity.Status = dto.Status ?? entity.Status;
        entity.TotalPrice = dto.TotalPrice ?? entity.TotalPrice;
        entity.CommissionAmount = dto.CommissionAmount ?? entity.CommissionAmount;
        entity.OrderId = dto.OrderId ?? entity.OrderId;

        if (dto.ImageUrls is not null)
        {
            entity.Images = dto.ImageUrls
                .Where(url => !string.IsNullOrWhiteSpace(url))
                .Select(url => new ProductImage { ImageUrl = url })
                .ToList();
        }
    

} 
}