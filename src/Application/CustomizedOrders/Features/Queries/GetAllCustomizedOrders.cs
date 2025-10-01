using Application.Common.Models;
using Application.Customers.DTOs;
using Application.CustomizedOrders.DTOs;
using Application.Delivery.DTOs;
using Application.Interfaces.CustomizedOrderInterfaces;

namespace Application.CustomizedOrders.Features.Queries;

public partial class CustomizedOrderQueries : ICustomizedOrderQueries
{
    public Task<Result<IEnumerable<CustomizedOrderResponse>>> GetAllCustomizedOrdersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Result<CustomizedOrderResponse>> GetCustomizedOrderByIDAsync(int id)
    {
        throw new NotImplementedException();
    }
}