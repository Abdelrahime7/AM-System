using Application.Common.Models;
using Application.Customers.DTOs;
using Application.Interfaces.CustomerInterfaces;

namespace Application.Customers.Features.Queries;

public partial class CustomerQueries
{
    public async Task<Result<CustomerResponse>> GetByNameAsync(string name)
    {
        try
        {
            var customer = await _repository.GetByNameAsync(name);
            if(customer == null)
                return Result<CustomerResponse>.Failure("No Customer Found");

            var response = _mapper.ToResponse(customer);
            return Result<CustomerResponse>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<CustomerResponse>.Failure($"failed to fetch customer: {ex.Message}");
        }
    }
}