using Application.Common.Models;
using Application.Customers.DTOs;
using Application.CustomizedOrders.DTOs;
using Application.Delivery.DTOs;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.CustomizedOrderInterfaces;
using Application.Interfaces.Repositories;
using Application.OrderDetails.DTOs;
using Domain.Entities;

namespace Application.CustomizedOrders.Features.Queries;

public partial class CustomizedOrderQueries( ICustomizedOrderRepository repository,
     IEntityMapper<CustomizedOrder, CreateCustomizedOrderRequest, UpdateCustomizedOrderRequest,
      CustomizedOrderResponse> mapper) : ICustomizedOrderQueries
{
    private readonly ICustomizedOrderRepository _repository = repository;
    private IEntityMapper<CustomizedOrder, CreateCustomizedOrderRequest, UpdateCustomizedOrderRequest,
     CustomizedOrderResponse> _mapper = mapper;

    public  async Task<Result<IEnumerable<CustomizedOrderResponse>>> GetAllCustomizedOrdersAsync()
    {

        try
        {
            var customizedOrders = await _repository.GetAllAsync();
            if (!customizedOrders.Any())
                return Result<IEnumerable<CustomizedOrderResponse>>.Failure("No details Found");

            var response = customizedOrders.ToList().Select(c => _mapper.ToResponse(c));
            return Result<IEnumerable<CustomizedOrderResponse>>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<CustomizedOrderResponse>>.Failure($"failed to fetch customers: {ex.Message}");
        }

    }

    public Task<Result<CustomizedOrderResponse>> GetCustomizedOrderByIDAsync(int id)
    {
        throw new NotImplementedException();
    }
}