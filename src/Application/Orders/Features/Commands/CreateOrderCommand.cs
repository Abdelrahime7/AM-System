using Application.Common.Models;
using Application.Delivery.DTOs;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.CustomerInterfaces;
using Application.Interfaces.CustomizedOrderInterfaces;
using Application.Interfaces.DeliveryInterfaces;
using Application.Interfaces.OrderDetailInterfaces;
using Application.Interfaces.OrderInterfaces;
using Application.Interfaces.Repositories;
using Application.Orders.DTOs;
using Domain.Entities;

namespace Application.Orders.Features.Commands;

public partial class OrderCommands( IOrderRepository orderRepository,ICustomerCommands Customercommands,
    ICustomizedOrderCommands customizedOrderCommands,
    IOrderDetailCommands orderDetailCommands,
    IEntityMapper<Order, CreateOrderRequest, UpdateOrderRequest,
       OrderResponse> mapper) : IOrderCommands
{
  
    private readonly IOrderRepository _OrderRepository= orderRepository;
    private readonly ICustomerCommands _Customercommands = Customercommands;
    private readonly ICustomizedOrderCommands _customizedOrderCommands = customizedOrderCommands;
    private readonly IOrderDetailCommands _orderDetailCommands = orderDetailCommands;


    private readonly IEntityMapper<Order, CreateOrderRequest
        , UpdateOrderRequest, OrderResponse> _mapper = mapper;




    public async Task<Result<int>> CreatOrderAsync(OrderSession CreatOrdersession)
    {
        if (CreatOrdersession == null || CreatOrdersession.Order == null)
        {
            return Result<int>.Failure($"Error creating an Order");
        }
       
       
        try
        {
            if (CreatOrdersession.Customer != null)
            {
                var Customer = await _Customercommands.CreateCustomerAsync(CreatOrdersession.Customer);
            }
            var order = _mapper.ToEntity(CreatOrdersession.Order);
            await _OrderRepository.AddAsync(order);
             
            if (CreatOrdersession.OrderDetails.Count != 0)
            {
                CreatOrdersession.OrderDetails.ForEach(D => D!.OrderId = order.Id);
                _Customercommands.
            }
            if (CreatOrdersession.Customizations.Count != 0)
            {

            }

            return Result<int>.Success(order.Id);
        }
        catch (Exception ex)
        {
            return Result<int>.Failure($"Error creating customer: {ex.Message}");
        }

    }

  

   
}
