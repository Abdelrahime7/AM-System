using Application.Common.Models;

namespace Application.Customers.Features.Commands;

public partial class DeliveryIntgrationCommands
{
    public async Task<Result<bool>> DeleteDeliveryIntegrationAsync(int id)
    {
        try
        {
            var delivery = await _repository.GetByIdAsync(id);
            if (delivery == null)
                return Result<bool>.Failure("Delivery Integration Not Found");
            else
                _repository.Delete(delivery);
            
            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure($"failed to delete Delivery Integration: {ex.Message}");
        }
    }
}