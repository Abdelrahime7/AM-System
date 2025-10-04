using Application.Common.Models;
using Application.Customers.DTOs;
using Application.Delivery.DTOs;
using Application.OrderDetails.DTOs;

namespace Application.OrderDetails.Features.Commands;

public partial class OrderDetailCommands
{
    public Task<Result<bool>> UpdateOrderDetailAsync(UpdateOrderDetailRequest request)
    {
        throw new NotImplementedException();
    }
}
