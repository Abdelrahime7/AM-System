namespace Application.CallsLog.DTOs;

public record CallLogrResponse
{
    public int Id { get; set; }

    public string CustomerPhone { get; set; } = string.Empty;

    public string CallResult { get; set; } = string.Empty;

    public DateTime CalledAt { get; set; }

    public int OrderId { get; set; }

    public string OrderReference { get; set; } = string.Empty;

    public int AgentId { get; set; }

    public string AgentName { get; set; } = string.Empty;

}



