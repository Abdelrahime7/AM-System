using Application.Common.Models;
using Application.Customers.DTOs;
using Application.Delivery.DTOs;
using Application.Interfaces.OrderDetailInterfaces;
using Application.OrderDetails.DTOs;

namespace Application.OrderDetails.Features.Queries;

public partial class OrderDetailQueries : IOrderDetailQueries
{
    public Task<Result<IEnumerable<OrderDetailResponse>>> GetAllOrderDetailsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Result<OrderDetailResponse>> GetOrderDetailByIDAsync(int id)
    {
        throw new NotImplementedException();
    }
}