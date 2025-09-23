namespace Domain.Entities;

public class Customer
{
    public int Id { get; set; }
    public required string FullName { get; set; }
    public required string City { get; set; }
    public string? Address { get; set; }
    public required string Phone { get; set; }

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}