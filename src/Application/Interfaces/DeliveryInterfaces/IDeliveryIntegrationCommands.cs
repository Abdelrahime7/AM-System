using Application.Common.Models;
using Application.Customers.DTOs;
using Application.Delivery.DTOs;

namespace Application.Interfaces.CustomerInterfaces;

public interface IDeliveryIntegrationCommands
{
    Task<Result<int>> CreateDeliveryIntegrationAsync(CreateDeliveryIntegrationRequest request);
    Task<Result<bool>> UpdateDeliveryIntegrationAsync(UpdateDeliveryIntegrationRequest request);
    Task<Result<bool>> DeleteDeliveryIntegrationAsync(int id);
}