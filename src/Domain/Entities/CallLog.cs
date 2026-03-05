using Domain.Enums;

namespace Domain.Entities;

public class CallLog
{
    public int Id { get; set; }
    public required string CustomerPhone { get; set; }
    public CallResult CallResult { get; set; }
    public DateTime CalledAt { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;
    
    public int AgentId { get; set; }
    public User Agent { get; set; } = null!;
}