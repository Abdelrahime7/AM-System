using Application.Common.Models;
using Application.Customers.DTOs;
using Application.Delivery.DTOs;
using Application.Orders.DTOs;
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

            if (request.OrderDetails!=null || request.OrderDetails.Any())
            {
                request.OrderDetails.ForEach(async D =>
                  await  _orderDetailCommands.UpdateOrderDetailAsync(D));
              
            }
            if (request.Customizations!=null || request.Customizations.Any())
            {
                request.Customizations.ForEach(async C =>
               await _customizedOrderCommands.UpdateCustomizedOrderAsync(C));
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
