using Application.Common.Models;
using Application.Customers.DTOs;
using Application.Customers.Mapper;
using Application.CustomizedOrders.DTOs;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.CustomerInterfaces;
using Application.Interfaces.OrderInterfaces;
using Application.Interfaces.Repositories;
using Application.OrderDetails.DTOs;
using Application.Orders.DTOs;
using Application.Orders.DTOs.Session;
using Domain.Entities;

namespace Application.Orders.Features.Queries;

public partial class OrderQueries (IOrderRepository repository,
   IEntityMapper<Order,ChangeOrderStatus,UpdateOrderRequest
       ,OrderResponse> mapper,

     IEntityMapper<Customer, CreateCustomerRequest, UpdateCustomerRequest
       , CustomerResponse> customermapper,

      IEntityMapper<CustomizedOrder, CreateCustomizedOrderRequest, UpdateCustomizedOrderRequest
       , CustomizedOrderResponse> customizedOrdermapper,

       IEntityMapper<OrderDetail, CreateOrderDetailRequest, UpdateOrderDetailRequest
       , OrderDetailResponse> orderDetailmapper
        ) : IOrderQueries


{

    private readonly IOrderRepository _repository= repository;
 

    private readonly IEntityMapper<Order, ChangeOrderStatus,
        UpdateOrderRequest, OrderResponse> _mapper= mapper;

    private readonly IEntityMapper<Customer, CreateCustomerRequest, UpdateCustomerRequest
     , CustomerResponse> _customermapper = customermapper;

    private readonly IEntityMapper<CustomizedOrder, CreateCustomizedOrderRequest, UpdateCustomizedOrderRequest
       , CustomizedOrderResponse> _customizedOrdermapper = customizedOrdermapper;

     private readonly IEntityMapper<OrderDetail, CreateOrderDetailRequest, UpdateOrderDetailRequest
       , OrderDetailResponse> _orderDetailmapper= orderDetailmapper;

    public async Task<Result<ResponseSession>> GetOrderByIDAsync(int id)
    {
        try
        {
            var order = await _repository.GetByIdAsync(id);
            if (order == null)
                return Result<ResponseSession>.Failure("Order not found");

            var customerResponse = _customermapper.ToResponse(order.Customer);
            var orderResponse = _mapper.ToResponse(order);

            var detailResponses = order.OrderDetails?.Count > 0
                ? order.OrderDetails.Select(_orderDetailmapper.ToResponse).ToList()
                : new List<OrderDetailResponse>();

            var customizationResponses = order.Customizations?.Count > 0
                ? order.Customizations.Select(_customizedOrdermapper.ToResponse).ToList()
                : new List<CustomizedOrderResponse>();

            var response = new ResponseSession
            {
                Customer = customerResponse,
                Order = orderResponse,
                OrderDetails = detailResponses,
                Customizations = customizationResponses
            };

            return Result<ResponseSession>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<ResponseSession>.Failure($"Failed to fetch order: {ex.Message}");
        }
    }



}