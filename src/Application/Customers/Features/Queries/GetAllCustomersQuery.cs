using Application.Common.Models;
using Application.Customers.DTOs;

namespace Application.Customers.Features.Queries;

public partial class CustomerQueries 
{
    public async Task<Result<IEnumerable<CustomerResponse>>> GetAllAsync()
    {
        try
        {
            var customers = await _repository.GetAllAsync();
            if(!customers.Any())
                return Result<IEnumerable<CustomerResponse>>.Failure("No Customers Found");

            var response = customers.ToList().Select(c => _mapper.ToResponse(c));
            return Result<IEnumerable<CustomerResponse>>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<CustomerResponse>>.Failure($"failed to fetch customers: {ex.Message}");
        }
    }
}