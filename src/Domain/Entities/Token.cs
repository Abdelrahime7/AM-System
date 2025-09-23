namespace Domain.Entities;

public class Token
{
    public int Id { get; set; }
    public required string TokenValue { get; set; }
    public DateTime ExpiresAt { get; set; }
    public DateTime? UsedAt { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;
}