using Domain.Enums;

namespace Application.CallsLog.DTOs;

public record UpdateCallLogRequest
{
    public string? CustomerPhone { get; set; }

    public CallResult? CallResult { get; set; }

    public DateTime? CalledAt { get; set; }

    public int? OrderId { get; set; }

    public int? AgentId { get; set; }
}