using Domain.Entities;

namespace Application.Customers.DTOs;

public record CustomerResponse
{
    public int Id { get; set; }
    public required string FullName { get; set; }
    public required string City { get; set; }
    public string? Address { get; set; }
    public required string Phone { get; set; }
}