using Application.Common.Models;
using Domain.Entities;
using Domain.Enums;

namespace Application.Orders.Features.Commands;

public partial class OrderCommands
{
    public async Task<Result<bool>> ChangeOrderStatusAsync(Order order,OrderStatus status)
    {
        try
        {

            if (order == null)
                return Result<bool>.Failure("No order found");

            order.Status = status;
            _OrderRepository.Update(order);

            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure($"Failed to change order status : {ex.Message}");
        }
    }


}