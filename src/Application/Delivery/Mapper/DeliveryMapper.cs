using Application.Delivery.DTOs;
using Application.Interfaces.Common.Mappers;
using Domain.Entities;

namespace Application.Customers.Mapper;

public class DeliveryMapper : IEntityMapper<DeliveryIntegration, CreateDeliveryIntegrationRequest,
    UpdateDeliveryIntegrationRequest, DeliveryIntegrationResponse>
{
    public DeliveryIntegration ToEntity(CreateDeliveryIntegrationRequest dto)
    {
        return new DeliveryIntegration
        {
         Name=dto.Name,
         ApiKey=dto.ApiKey,
         ApiSecret=dto.ApiSecret,
         ApiEndpoint=dto.ApiEndpoint,
         IsActive=dto.IsActive,
 
        };
    }

    public DeliveryIntegrationResponse ToResponse(DeliveryIntegration entity)
    {
        return new DeliveryIntegrationResponse
        {
            Name = entity.Name,
            ApiEndpoint = entity.ApiEndpoint,
            IsActive = entity.IsActive,
        };
    }

    

    public void ToUpdateEntity(DeliveryIntegration delivery, UpdateDeliveryIntegrationRequest dto)
    {
        delivery.Name        = dto.Name        ?? delivery.Name;
        delivery.ApiKey      = dto.ApiKey      ?? delivery.ApiKey;
        delivery.ApiSecret   = dto.ApiSecret   ?? delivery.ApiSecret;
        delivery.ApiEndpoint = dto.ApiEndpoint ?? delivery.ApiEndpoint;

        if (dto.IsActive.HasValue)
            delivery.IsActive = dto.IsActive.Value;
    }

    public DeliveryIntegration ToUpdateEntity(UpdateDeliveryIntegrationRequest dto)
    {
        throw new NotImplementedException();
    }
}