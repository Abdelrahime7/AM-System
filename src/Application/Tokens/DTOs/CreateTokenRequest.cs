
namespace Application.Tokens.DTOs;


public record CreateTokenRequest
{
    public required string TokenValue { get; set; } = string.Empty;

    public DateTime ExpiresAt { get; set; }

    public int UserId { get; set; }
}


