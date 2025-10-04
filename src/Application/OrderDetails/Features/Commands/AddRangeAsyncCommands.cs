using Application.Common.Models;
using Application.Customers.DTOs;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.OrderDetailInterfaces;
using Application.Interfaces.Repositories;
using Application.OrderDetails.DTOs;
using Domain.Entities;

namespace Application.OrderDetails.Features.Commands;

public partial class OrderDetailCommands (IOrderDetailRepository repository,
    IEntityMapper<OrderDetail, CreateOrderDetailRequest, UpdateOrderDetailRequest, OrderDetailResponse> mapper) : IOrderDetailCommands
{
    private readonly IOrderDetailRepository _repository= repository;
    private IEntityMapper<OrderDetail, CreateOrderDetailRequest,
        UpdateOrderDetailRequest, OrderDetailResponse> _mapper = mapper;
    public async Task<Result> AddRangeAsync(List<CreateOrderDetailRequest> orderDetailRequests)
    {
        try
        {
            List<OrderDetail> details = orderDetailRequests
                                    .Select(_mapper.ToEntity)
                                    .ToList();

           await _repository.AddRangeAsync(details);
            return Result.Success();

        }
        catch (Exception ex)
        {
            return Result.Failure($"Failed to create order Details. Reason: {ex.Message}");
        }

    }
}