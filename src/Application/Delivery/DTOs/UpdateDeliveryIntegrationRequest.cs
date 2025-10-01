namespace Application.Delivery.DTOs;

public record UpdateDeliveryIntegrationRequest
{
    public int Id { get; set; }
    public string?Name { get; set; }
    public string? ApiEndpoint { get; set; }
    public string? ApiKey { get; set; }
    public string? ApiSecret { get; set; }
    public bool  ?IsActive { get; set; }

}