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
        throw new NotImplementedException();
    }

    public CustomizedOrderResponse ToResponse(CustomizedOrder entity)
    {
        throw new NotImplementedException();
    }

    public void ToUpdateEntity(CustomizedOrder entity, UpdateCustomizedOrderRequest dto)
    {
        throw new NotImplementedException();
    }
}