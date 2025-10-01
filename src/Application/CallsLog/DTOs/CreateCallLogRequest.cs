using Domain.Enums;

namespace Application.CallsLog.DTOs;

public record CreateRoleRequest
{
    public required string CustomerPhone { get; set; } = string.Empty;

    public required CallResult CallResult { get; set; }

    public DateTime CalledAt { get; set; }

    public  required int OrderId { get; set; }

    public  required int AgentId { get; set; }

}