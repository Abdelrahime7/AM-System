using Application.Common.Models;
using Application.Customers.DTOs;
using Application.Delivery.DTOs;
using Application.OrderDetails.DTOs;

namespace Application.OrderDetails.Features.Commands;

public partial class OrderDetailCommands
{
    public async Task<Result<bool>> UpdateOrderDetailAsync(UpdateOrderDetailRequest request)
    {

        try
        {
            var orderDetail = await _repository.GetByIdAsync(request.Id);
            if (orderDetail == null)
                return Result<bool>.Failure("orderDetail Not Found");

            _mapper.ToUpdateEntity(orderDetail, request);
            _repository.Update(orderDetail);
            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure($"failed to update orderDetail: {ex.Message}");
        }
    }
}

