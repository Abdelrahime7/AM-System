namespace Application.Delivery.DTOs;

public record CreateDeliveryIntegrationRequest
{
    public string? Name { get; set; }
    public string? ApiEndpoint { get; set; }
    public string? ApiKey { get; set; }
    public string? ApiSecret { get; set; }
    public bool IsActive { get; set; }

}