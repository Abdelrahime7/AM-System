using Application.Common.Models;
using Application.Orders.DTOs;
using Domain.Entities;
using Domain.Enums;

namespace Application.Orders.Features.Commands;

public partial class OrderCommands
{
    public async Task<Result<bool>> ChangeOrderStatusAsync(UpdateOrderRequest request)
    {
        try
        {
            var order = await _UnitOfWork._orderRepository.GetByIdAsync(request.OrderId);
            if (order == null)
                return Result<bool>.Failure("No order found");

            _UnitOfWork._orderRepository.Update(order);

            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure($"Failed to change order status : {ex.Message}");
        }
    }


}