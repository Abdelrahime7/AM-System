using Application.Common.Models;
using Application.Delivery.DTOs;

namespace Application.Interfaces.CustomerInterfaces;

public interface IDeliveryIntegrationQueries
{
    Task<Result<IEnumerable<DeliveryIntegrationResponse>>> GetAllAsync();
    Task<Result<DeliveryIntegrationResponse>> GetByIdAsync(int id);
}