using Application.AuditsLog.DTOs;
using Application.CustomizedOrders.DTOs;
using Application.Interfaces.Common.Mappers;
using Application.OrderDetails.DTOs;
using Domain.Entities;


namespace Application.OrderDetails.Mapper;

public class OrderDetailMapper : IEntityMapper<OrderDetail, CreateOrderDetailRequest,
    UpdateOrderDetailRequest, OrderDetailResponse>
{
    public OrderDetail ToEntity(CreateOrderDetailRequest request)
    {
        return new OrderDetail
        {
            Quantity = request.Quantity,
            UnitPrice = request.UnitPrice,
            UnitCommission = request.UnitCommission,
            TotalPrice = request.TotalPrice,
            TotalCommission = request.TotalCommission,
            ProductId = request.ProductId
            // OrderId and navigation properties should be assigned externally
        };
    }


    public OrderDetailResponse ToResponse(OrderDetail entity)
    {
        return new OrderDetailResponse
        {
            Id = entity.Id,
            Quantity = entity.Quantity,
            UnitPrice = entity.UnitPrice,
            UnitCommission = entity.UnitCommission,
            TotalPrice = entity.TotalPrice,
            TotalCommission = entity.TotalCommission,
            OrderId = entity.OrderId,
            OrderRef = entity.Order?.OrderRef ?? string.Empty,
            ProductId = entity.ProductId,
            ProductName = entity.Product?.Name ?? string.Empty
        };
    }



    public void ToUpdateEntity(OrderDetail entity, UpdateOrderDetailRequest dto)
    {
        entity.Quantity = dto.Quantity ?? entity.Quantity;
        entity.UnitPrice = dto.UnitPrice ?? entity.UnitPrice;
        entity.UnitCommission = dto.UnitCommission ?? entity.UnitCommission;
        entity.TotalPrice = dto.TotalPrice ?? entity.TotalPrice;
        entity.TotalCommission = dto.TotalCommission ?? entity.TotalCommission;
        entity.OrderId = dto.OrderId ?? entity.OrderId;
        entity.ProductId = dto.ProductId ?? entity.ProductId;
    }

}