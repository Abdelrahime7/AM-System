using Application.Common.Models;
using Application.Customers.DTOs;
using Application.CustomizedOrders.DTOs;
using Application.Delivery.DTOs;

namespace Application.CustomizedOrders.Features.Commands;

public partial class CustomizedOrderCommands
{
    public async Task<Result<bool>> UpdateCustomizedOrderAsync(UpdateCustomizedOrderRequest request)
    {
        try
        {
            var customizedOrder = await _repository.GetByIdAsync(request.Id);
            if (customizedOrder == null)
                return Result<bool>.Failure("customized Order Not Found");

            _mapper.ToUpdateEntity(customizedOrder, request);
            _repository.Update(customizedOrder);
            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure($"failed to update customized Order: {ex.Message}");
        }
    }
}
