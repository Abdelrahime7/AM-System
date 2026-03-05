namespace Application.Delivery.DTOs;

public record CreateDeliveryIntegrationRequest
{
    public required string  Name { get; set; }
    public required string ApiEndpoint { get; set; }
    public required string ApiKey { get; set; }
    public required string ApiSecret { get; set; }
    public bool IsActive { get; set; }

}