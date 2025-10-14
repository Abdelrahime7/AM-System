namespace Application.CustomizedOrders.DTOs;

public record CustomizedOrderResponse
{
    public int id {  get; set; }
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string Dimensions { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public decimal TotalPrice { get; set; }

    public decimal CommissionAmount { get; set; }

    public int OrderId { get; set; }

    public string OrderReference { get; set; } = string.Empty;

    public List<string> ImageUrls { get; set; } = new();

}



