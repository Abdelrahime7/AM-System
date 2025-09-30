using Application.Common.Models;
using Application.Delivery.DTOs;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.DeliveryInterfaces;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.Customers.Features.Commands;

public partial class DeliveryIntgrationCommands(
    IDeliveryRepository repository,
    IEntityMapper<DeliveryIntegration, CreateDeliveryIntegrationRequest,
        UpdateDeliveryIntegrationRequest, DeliveryIntegrationResponse> mapper)
    : IDeliveryIntegrationCommands
{
    private readonly IDeliveryRepository _repository = repository;
    private readonly IEntityMapper<DeliveryIntegration, CreateDeliveryIntegrationRequest,
        UpdateDeliveryIntegrationRequest, DeliveryIntegrationResponse> _mapper = mapper;
    
    public async Task<Result<int>> CreateDeliveryIntegrationAsync(CreateDeliveryIntegrationRequest request)
    {
        try
        {
            var Delivery = _mapper.ToEntity(request);
            await _repository.AddAsync(Delivery);
            return Result<int>.Success(Delivery.Id);
        }
        catch (Exception ex)
        {
            return Result<int>.Failure($"Error creating Delivery Integration: {ex.Message}");
        }
    }

  
}
