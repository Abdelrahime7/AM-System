using Application.Common.Models;
using Application.Customers.DTOs;
using Application.Delivery.DTOs;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.CustomerInterfaces;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.Customers.Features.Queries;

public partial class DeliveryIntegrationQueries(
    IDeliveryRepository repository,
    IEntityMapper<DeliveryIntegration, CreateDeliveryIntegrationRequest,
        UpdateDeliveryIntegrationRequest, DeliveryIntegrationResponse> mapper)
    : IDeliveryIntegrationQueries
{
    private readonly IDeliveryRepository _repository = repository;
    private readonly IEntityMapper<DeliveryIntegration, CreateDeliveryIntegrationRequest,
        UpdateDeliveryIntegrationRequest, DeliveryIntegrationResponse> _mapper = mapper;

   
    public async Task<Result<DeliveryIntegrationResponse>> GetByIdAsync(int id)
    {
        try
        {
            var delivery = await _repository.GetByIdAsync(id);
            if(delivery == null)
                return Result<DeliveryIntegrationResponse>.Failure("No Delivery Integration Found");

            var response = _mapper.ToResponse(delivery);
            return Result<DeliveryIntegrationResponse>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<DeliveryIntegrationResponse>.Failure($"failed to fetch  Delivery Integration: {ex.Message}");
        }
    }

   
}