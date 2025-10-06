using Application.Common.Models;
using Application.Customers.DTOs;
using Application.Delivery.DTOs;
using Application.Orders.DTOs.Session;
using Domain.Entities;

namespace Application.Orders.Features.Commands;

public partial class OrderCommands
{
    public  async Task<Result<bool>> UpdateOrderAsync(UpdateOrderSession request)
    {
        if (request == null)
            return Result<bool>.Failure("Invalid update request.");

        try
        {

            if (request.Order != null)
            {
                var Order = await _OrderRepository.GetByIdAsync(request.Order.OrderId);
                if (Order == null)
                    return Result<bool>.Failure("Order Not Found");
                
                _mapper.ToUpdateEntity(Order, request.Order);
                _OrderRepository.Update(Order);
            }

            if (request.OrderDetails != null && request.OrderDetails.Any())
            {
                foreach (var detail in request.OrderDetails)
                {
                    await _orderDetailCommands.UpdateOrderDetailAsync(detail);
                }
            }

            if (request.Customizations != null && request.Customizations.Any())
            {
                foreach (var customization in request.Customizations)
                {
                    await _customizedOrderCommands.UpdateCustomizedOrderAsync(customization);
                }
            }

            await _OrderRepository.CommitAsync();

            return Result<bool>.Success(true);

        }
        catch (Exception ex)
        {
            return Result<bool>.Failure($"failed to update Order: {ex.Message}");
        }
    }

}
