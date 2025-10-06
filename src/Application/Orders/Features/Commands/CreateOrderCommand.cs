using Application.Common.Models;
using Application.Delivery.DTOs;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.CustomerInterfaces;
using Application.Interfaces.CustomizedOrderInterfaces;
using Application.Interfaces.OrderDetailInterfaces;
using Application.Interfaces.OrderInterfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWorks;
using Application.Orders.Delivery;
using Application.Orders.DTOs;
using Application.Orders.DTOs.Session;
using Domain.Entities;

namespace Application.Orders.Features.Commands;

public partial class OrderCommands( IOrderUnitOfWork unitOfWork,
    ILocalDeliveryStrategy local,
    IExternallDeliverStrategy external,
    IEntityMapper<Order, CreateOrderRequest, UpdateOrderRequest,
       OrderResponse> mapper) : IOrderCommands
{

    private readonly IOrderUnitOfWork _UnitOfWork=unitOfWork; 

    private readonly ILocalDeliveryStrategy _local = local;
    private readonly IExternallDeliverStrategy _external = external;

    private readonly IEntityMapper<Order, CreateOrderRequest
        , UpdateOrderRequest, OrderResponse> _mapper = mapper;




    public async Task<Result<int>> CreatOrderAsync(CreatOrderSession createOrderSession)
    {
        if (createOrderSession?.Order == null || createOrderSession.Customer == null)
            return Result<int>.Failure("No order requests provided.");

        try
        {
            var customerResult =  await _UnitOfWork.Customers.CreateCustomerAsync(createOrderSession.Customer);
            if (!customerResult.IsSuccess )
                return Result<int>.Failure("Customer creation failed.");

            createOrderSession.Order.CustomerId = customerResult.Value;

            var order = _mapper.ToEntity(createOrderSession.Order);
            await _UnitOfWork.orderRepository.AddAsync(order);

            if (createOrderSession.OrderDetails?.Any() == true)
            {
                createOrderSession.OrderDetails
                    .Where(d => d != null)
                    .ToList()
                    .ForEach(d => d.OrderId = order.Id);

                await _UnitOfWork.OrderDetails.AddRangeAsync(createOrderSession.OrderDetails);
            }

            if (createOrderSession.Customizations?.Any() == true)
            {
                createOrderSession.Customizations
                    .Where(c => c != null)
                    .ToList()
                    .ForEach(c => c.OrderId = order.Id);

                await _UnitOfWork.CustomizedOrders.AddRangeAsync(createOrderSession.Customizations);
            }

            await _UnitOfWork.SaveChangesAsync();
            
            return Result<int>.Success(order.Id);
        }
        catch (Exception ex)
        {
            // Optionally log ex
            return Result<int>.Failure($"Failed to create order: {ex.Message}");
        }

    }





}
