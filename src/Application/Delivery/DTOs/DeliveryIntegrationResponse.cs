using Domain.Entities;

namespace Application.Delivery.DTOs; 
public record DeliveryIntegrationResponse
{
    public string? Name { get; set; }
    public string? ApiEndpoint { get; set; }
    public bool IsActive { get; set; }
}