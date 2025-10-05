using Application.Common.Models;
using Application.Delivery.DTOs;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.CustomerInterfaces;
using Application.Interfaces.CustomizedOrderInterfaces;
using Application.Interfaces.OrderDetailInterfaces;
using Application.Interfaces.OrderInterfaces;
using Application.Interfaces.Repositories;
using Application.Orders.Delivery;
using Application.Orders.DTOs;
using Application.Orders.DTOs.Session;
using Domain.Entities;

namespace Application.Orders.Features.Commands;

public partial class OrderCommands( IOrderRepository orderRepository,ICustomerCommands Customercommands,
    ICustomizedOrderCommands customizedOrderCommands,
    IOrderDetailCommands orderDetailCommands,
    LocalDriverDelivery local,
    ExternalCompanyDeliveryStrategy external,
    IEntityMapper<Order, CreateOrderRequest, UpdateOrderRequest,
       OrderResponse> mapper) : IOrderCommands
{
  
    private readonly IOrderRepository _OrderRepository= orderRepository;
    private readonly ICustomerCommands _Customercommands = Customercommands;
    private readonly ICustomizedOrderCommands _customizedOrderCommands = customizedOrderCommands;
    private readonly IOrderDetailCommands _orderDetailCommands = orderDetailCommands;

    private readonly LocalDriverDelivery _local = local;
    private readonly ExternalCompanyDeliveryStrategy _external= external;

    private readonly IEntityMapper<Order, CreateOrderRequest
        , UpdateOrderRequest, OrderResponse> _mapper = mapper;




    public async Task<Result<int>> CreatOrderAsync(CreatOrderSession createOrderSession)
    {
        if (createOrderSession?.Order == null || createOrderSession.Customer == null)
            return Result<int>.Failure("No order requests provided.");

        try
        {
            var customerResult = await _Customercommands.CreateCustomerAsync(createOrderSession.Customer);
            if (!customerResult.IsSuccess || customerResult.Value == default)
                return Result<int>.Failure("Customer creation failed.");

            createOrderSession.Order.CustomerId = customerResult.Value;
            var order = _mapper.ToEntity(createOrderSession.Order);
            await _OrderRepository.AddAsync(order);

            if (createOrderSession.OrderDetails?.Any() == true)
            {
                createOrderSession.OrderDetails
                    .Where(d => d != null)
                    .ToList()
                    .ForEach(d => d.OrderId = order.Id);

                await _orderDetailCommands.AddRangeAsync(createOrderSession.OrderDetails);
            }

            if (createOrderSession.Customizations?.Any() == true)
            {
                createOrderSession.Customizations
                    .Where(c => c != null)
                    .ToList()
                    .ForEach(c => c.OrderId = order.Id);

                await _customizedOrderCommands.AddRangeAsync(createOrderSession.Customizations);
            }

           await _OrderRepository.CommitAsync();
            return Result<int>.Success(order.Id);
        }
        catch (Exception ex)
        {
            // Optionally log ex
            return Result<int>.Failure($"Failed to create order: {ex.Message}");
        }

    }





}
