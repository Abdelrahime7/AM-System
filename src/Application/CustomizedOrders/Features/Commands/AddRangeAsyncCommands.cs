using Application.Common.Models;
using Application.CustomizedOrders.DTOs;
using Application.Delivery.DTOs;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.CustomizedOrderInterfaces;
using Application.Interfaces.DeliveryInterfaces;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.CustomizedOrders.Features.Commands;

public partial class CustomizedOrderCommands(ICustomizedOrderRepository repository,
     IEntityMapper<CustomizedOrder,CreateCustomizedOrderRequest,UpdateCustomizedOrderRequest
         ,CustomizedOrderResponse> mapper) : ICustomizedOrderCommands
{
    private readonly ICustomizedOrderRepository _repository = repository;
    private readonly IEntityMapper<CustomizedOrder, CreateCustomizedOrderRequest,
        UpdateCustomizedOrderRequest
        , CustomizedOrderResponse> _mapper = mapper;



    public async Task<Result> AddRangeAsync(List<CreateCustomizedOrderRequest> orderRequests)
    {
        try
        {
            List<CustomizedOrder> customizedsOrder = orderRequests
                                    .Select(_mapper.ToEntity)
                                    .ToList();

            await _repository.AddRangeAsync(customizedsOrder);
            return Result.Success();

        }
        catch (Exception ex)
        {
            return Result.Failure($"Failed to create customized orders. Reason: {ex.Message}");

        }
    }


  
}
