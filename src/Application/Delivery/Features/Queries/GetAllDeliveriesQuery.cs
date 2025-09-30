using Application.Common.Models;
using Application.Customers.DTOs;
using Application.Delivery.DTOs;

namespace Application.Customers.Features.Queries;

public partial class DeliveryIntegrationQueries
{
    public async Task<Result<IEnumerable<DeliveryIntegrationResponse>>> GetAllAsync()
    {
        try
        {
            var Dilveries = await _repository.GetAllAsync();
            if(!Dilveries.Any())
                return Result<IEnumerable<DeliveryIntegrationResponse>>.Failure("No Dilveries integration Found");

            var response = Dilveries.ToList().Select(c => _mapper.ToResponse(c));
            return Result<IEnumerable<DeliveryIntegrationResponse>>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<DeliveryIntegrationResponse>>.Failure($"failed to fetch Dilveries integration: {ex.Message}");
        }
    }
}