namespace Application.Customers.DTOs;

public record UpdateCustomerRequest
{
    public int Id { get; set; }
    public string? FullName { get; set; }
    public string? City { get; set; }
    public string? Address { get; set; }
    public required string Phone { get; set; }
}