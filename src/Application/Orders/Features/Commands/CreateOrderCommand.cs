using Application.Common.Models;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.OrderInterfaces;
using Application.Interfaces.UnitOfWorks;
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




    public async Task<Result<int>> CreateOrderAsync(CreatOrderSession createOrderSession)
    {
        if (createOrderSession?.Order == null || createOrderSession.Customer == null)
            return Result<int>.Failure("No order requests provided.");

        try
        {
            // Step 1: Create customer 
            var customerResult = await _UnitOfWork.Customers.CreateCustomerAsync(createOrderSession.Customer);
            if (!customerResult.IsSuccess)
                return Result<int>.Failure("Customer creation failed.");

           
            // Step 3: Map and prepare order
            var order = _mapper.ToEntity(createOrderSession.Order);
           order.Customer = customerResult.Value;
            

            await _UnitOfWork._orderRepository.AddAsync(order);

            // Step 4: Add order details if present
            if (createOrderSession.OrderDetails?.Any() == true)
            {
                foreach (var detail in createOrderSession.OrderDetails.Where(d => d != null))
                {
                    detail.OrderId = order.Id;
                }

                await _UnitOfWork.OrderDetails.AddRangeAsync(createOrderSession.OrderDetails);
            }

            // Step 5: Add customizations if present
            if (createOrderSession.Customizations?.Any() == true)
            {
                foreach (var customization in createOrderSession.Customizations.Where(c => c != null))
                {
                    customization.OrderId = order.Id;
                }

                await _UnitOfWork.CustomizedOrders.AddRangeAsync(createOrderSession.Customizations);
            }

            // Step 6: Commit all changes
            await _UnitOfWork.SaveChangesAsync();

            return Result<int>.Success(order.Id);
        }
        catch (Exception ex)
        {
            return Result<int>.Failure($"Failed to create order: {ex.Message}");
        }
    }






}
