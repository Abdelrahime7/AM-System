using Application.Common.Models;
using Application.Customers.DTOs;
using Application.Delivery.DTOs;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.OrderDetailInterfaces;
using Application.Interfaces.Repositories;
using Application.OrderDetails.DTOs;
using Domain.Entities;

namespace Application.OrderDetails.Features.Queries;

public partial class OrderDetailQueries(IOrderDetailRepository repository,
     IEntityMapper<OrderDetail,CreateOrderDetailRequest,UpdateOrderDetailRequest,
         OrderDetailResponse> mapper ) : IOrderDetailQueries
{
    private readonly IOrderDetailRepository _repository = repository;
    private IEntityMapper<OrderDetail, CreateOrderDetailRequest, UpdateOrderDetailRequest,
         OrderDetailResponse> _mapper = mapper;

    public  async Task<Result<IEnumerable<OrderDetailResponse>>> GetAllOrderDetailsAsync()
    {
        try
        {
            var details = await _repository.GetAllAsync();
            if (!details.Any())
                return Result<IEnumerable<OrderDetailResponse>>.Failure("No details Found");

            var response = details.ToList().Select(c => _mapper.ToResponse(c));
            return Result<IEnumerable<OrderDetailResponse>>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<OrderDetailResponse>>.Failure($"failed to fetch customers: {ex.Message}");
        }


    }

    public Task<Result<OrderDetailResponse>> GetOrderDetailByIDAsync(int id)
    {
        throw new NotImplementedException();
    }
}