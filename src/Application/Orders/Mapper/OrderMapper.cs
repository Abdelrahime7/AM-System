using Application.AuditsLog.DTOs;
using Application.CustomizedOrders.DTOs;
using Application.Interfaces.Common.Mappers;
using Application.Orders.DTOs;
using Domain.Entities;


namespace Application.Orders.Mapper;


public class OrderMapper : IEntityMapper<Order, CreateOrderRequest,
    UpdateOrderRequest, OrderResponse>
{
    public Order ToEntity(CreateOrderRequest dto)
    {
        throw new NotImplementedException();
    }

    

    public OrderResponse ToResponse(Order entity)
    {
        throw new NotImplementedException();
    }

   

    public void ToUpdateEntity(Order entity, UpdateOrderRequest dto)
    {
        throw new NotImplementedException();
    }
}