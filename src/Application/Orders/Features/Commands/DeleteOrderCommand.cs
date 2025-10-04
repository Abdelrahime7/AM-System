using Application.Common.Models;

namespace Application.Orders.Features.Commands;

public partial class OrderCommands
{
    public async Task<Result<bool>> DeleteOrderAsync(int id)
    {
        try
        {
            var Order = await _OrderRepository.GetByIdAsync(id);
            if (Order == null)
                return Result<bool>.Failure("Order Not Found");
            else
                _OrderRepository.Delete(Order);

            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure($"failed to delete Order: {ex.Message}");
        }
    }
}