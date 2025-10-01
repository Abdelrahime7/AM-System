using Domain.Enums;

namespace Application.Tokens.DTOs;

public record UpdateTokenRequest
{
    public string? TokenValue { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public DateTime? UsedAt { get; set; }

    public int? UserId { get; set; }
}
