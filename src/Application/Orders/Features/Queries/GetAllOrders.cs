using Application.Common.Models;
using Application.Interfaces.OrderInterfaces;
using Application.Orders.DTOs;

namespace Application.Orders.Features.Queries;

public partial class OrderQueries : IOrderQueries
{
    public Task<Result<IEnumerable<OrderResponse>>> GetAllOrdersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Result<OrderResponse>> GetOrderByIDAsync(int id)
    {
        throw new NotImplementedException();
    }
}