using Application.Common.Models;
using Application.Customers.DTOs;

namespace Application.Customers.Features.Commands;

public partial class CustomerCommands
{
    public async Task<Result<bool>> UpdateCustomerAsync(UpdateCustomerRequest request)
    {
        try
        {
            var customer = await _repository.GetByIdAsync(request.Id);
            if (customer == null)
                return Result<bool>.Failure("Customer Not Found");

            _mapper.ToUpdateEntity(customer, request); 
            _repository.Update(customer);
            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure($"failed to update customer: {ex.Message}");
        }
    }
}
