

namespace Application.Tokens.DTOs;


    public record TokenResponse
    {
    public int Id { get; set; }

    public string TokenValue { get; set; } = string.Empty;

    public DateTime ExpiresAt { get; set; }

    public DateTime? UsedAt { get; set; }

    public int UserId { get; set; }

    public string UserName { get; set; } = string.Empty;
}





