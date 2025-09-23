namespace Domain.Entities;

public class DeliveryIntegration
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? ApiEndpoint { get; set; }
    public string? ApiKey { get; set; }
    public string? ApiSecret { get; set; }
    public bool IsActive { get; set; }
}