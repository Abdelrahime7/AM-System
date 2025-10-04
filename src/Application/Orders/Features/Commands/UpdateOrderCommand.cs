using Application.Common.Models;
using Application.Customers.DTOs;
using Application.Delivery.DTOs;
using Application.Orders.DTOs;

namespace Application.Orders.Features.Commands;

public partial class OrderCommands
{
    public Task<Result<bool>> UpdateOrderAsync(UpdateOrderRequest request)
    {
        throw new NotImplementedException();
    }

}
