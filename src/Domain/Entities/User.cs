using Domain.Enums;

namespace Domain.Entities;

public class User
{
    public int Id { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public required string PasswordHash { get; set; }
    public string? CcpNumber { get; set; }
    public UserStatus Status { get; set; }
    public DateTime? LastLoginAt { get; set; }

    // Navigation properties
    public Role? Role { get; set; }
    public int? RoleId { get; set; }

    public List<CallLog> callLogs { get; set; } = new();
}