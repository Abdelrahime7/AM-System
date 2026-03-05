using Application.Common.Models;
using Application.Customers.DTOs;
using Application.Delivery.DTOs;

namespace Application.Customers.Features.Commands;

public partial class DeliveryIntgrationCommands
{
    public async Task<Result<bool>> UpdateDeliveryIntegrationAsync(UpdateDeliveryIntegrationRequest request)
    {
        try
        {
            var delivery = await _repository.GetByIdAsync(request.Id);
            if (delivery == null)
                return Result<bool>.Failure("Customer Not Found");

            _mapper.ToUpdateEntity(delivery, request); 
            _repository.Update(delivery);

            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure($"failed to update DeliveryIntegration: {ex.Message}");
        }
    }
}
