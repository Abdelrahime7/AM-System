using Application.AuditsLog.DTOs;
using Application.CustomizedOrders.DTOs;
using Application.Interfaces.Common.Mappers;
using Application.OrderDetails.DTOs;
using Domain.Entities;


namespace Application.OrderDetails.Mapper;

public class OrderDetailMapper : IEntityMapper<OrderDetail, CreateOrderDetailRequest,
    UpdateOrderDetailRequest, OrderDetailResponse>
{
    public OrderDetail ToEntity(CreateOrderDetailRequest dto)
    {
        throw new NotImplementedException();
    }

    public CustomizedOrderResponse ToResponse(AuditLog entity)
    {
        throw new NotImplementedException();
    }

    public OrderDetailResponse ToResponse(OrderDetail entity)
    {
        throw new NotImplementedException();
    }

    public void ToUpdateEntity(AuditLog entity, UpdateCustomizedOrderRequest dto)
    {
        throw new NotImplementedException();
    }

    public void ToUpdateEntity(OrderDetail entity, UpdateOrderDetailRequest dto)
    {
        throw new NotImplementedException();
    }
}