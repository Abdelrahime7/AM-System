using Application.Common.Models;

namespace Application.Customers.Features.Commands;

public partial class CustomerCommands
{
    public async Task<Result<bool>> DeleteCustomerAsync(int id)
    {
        try
        {
            var customer = await _repository.GetByIdAsync(id);
            if (customer == null)
                return Result<bool>.Failure("Customer Not Found");
            else
                _repository.Delete(customer);
            
            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure($"failed to delete customer: {ex.Message}");
        }
    }
}