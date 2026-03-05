namespace Domain.Entities;

public class RefereshToken
{
    public int Id { get; set; }
    public required string TokenValue { get; set; }
    public DateTime ExpiresAt { get; set; }
    public DateTime? CreatedAt { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime? RevokedAt { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
}