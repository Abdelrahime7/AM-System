using Application.AuditsLog.DTOs;
using Application.CustomizedOrders.DTOs;
using Application.Interfaces.Common.Mappers;
using Application.Orders.DTOs;
using Domain.Entities;
using Domain.Enums;


namespace Application.Orders.Mapper;


public class OrderMapper : IEntityMapper<Order, ChangeOrderStatus,
    UpdateOrderRequest, OrderResponse>
{
    public  Order ToEntity(ChangeOrderStatus dto)
    {
        return new Order
        {
            OrderRef = dto.OrderRef ?? string.Empty,
            Status = OrderStatus.Pending,
            IsCustomized = dto.IsCustomized,
            ReviewedAt = dto.ReviewedAt,
            DepartedAt = dto.DepartedAt,
            DeliveredAt = dto.DeliveredAt,
            AffiliateId = dto.AffiliateId,
           
            
            // Navigation properties and collections are left null or handled via separate hydration
            Affiliate = null!,
            Customer = null!,
            DeliveryCompany = null,
            Reviewer = null,
            OrderDetails = [],
            Customizations = []
        };


    }

    public  OrderResponse ToResponse(Order entity)
    {
        return new OrderResponse
        {
            Id = entity.Id,
            OrderRef = entity.OrderRef,
            OrderType = entity.OrderType.ToString(),
            Status = entity.Status.ToString(),
            IsCustomized = entity.IsCustomized,
            ReviewedAt = entity.ReviewedAt,
            DepartedAt = entity.DepartedAt,
            DeliveredAt = entity.DeliveredAt,

            AffiliateId = entity.AffiliateId,
            AffiliateName = entity.Affiliate?.FullName ?? string.Empty,

            CustomerId = entity.CustomerId,
            CustomerName = entity.Customer?.FullName ?? string.Empty,

            DriverId = entity.DriverId,
            DriverName = entity.Driver?.FullName,

            DeliveryCompanyId = entity.DeliveryCompanyId,
            DeliveryCompanyName = entity.DeliveryCompany?.Name,

            ReviewedBy = entity.ReviewedBy,
            ReviewerName = entity.Reviewer?.FullName
        };
    }

    public void ToUpdateEntity(Order entity, UpdateOrderRequest dto)
    {
        entity.OrderRef = dto.OrderRef ?? entity.OrderRef;
        entity.OrderType = dto.OrderType ?? entity.OrderType;
        entity.Status = dto.Status ?? entity.Status;
        entity.IsCustomized = dto.IsCustomized ?? entity.IsCustomized;
        entity.ReviewedAt = dto.ReviewedAt ?? entity.ReviewedAt;
        entity.DepartedAt = dto.DepartedAt ?? entity.DepartedAt;
        entity.DeliveredAt = dto.DeliveredAt ?? entity.DeliveredAt;
        entity.AffiliateId = dto.AffiliateId ?? entity.AffiliateId;
        entity.CustomerId = dto.CustomerId ?? entity.CustomerId;
        entity.DriverId = dto.DriverId ?? entity.DriverId;
        entity.DeliveryCompanyId = dto.DeliveryCompanyId ?? entity.DeliveryCompanyId;
        entity.ReviewedBy = dto.ReviewedBy ?? entity.ReviewedBy;
    }




}