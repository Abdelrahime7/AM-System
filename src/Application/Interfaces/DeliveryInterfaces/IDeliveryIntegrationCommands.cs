using Application.Common.Models;
using Application.Delivery.DTOs;

namespace Application.Interfaces.DeliveryInterfaces;

public interface IDeliveryIntegrationCommands
{
    Task<Result<int>> CreateDeliveryIntegrationAsync(CreateDeliveryIntegrationRequest request);
    Task<Result<bool>> UpdateDeliveryIntegrationAsync(UpdateDeliveryIntegrationRequest request);
    Task<Result<bool>> DeleteDeliveryIntegrationAsync(int id);
}