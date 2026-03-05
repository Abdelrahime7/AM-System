namespace Application.OrderDetails.DTOs;

public class OrderDetailResponse
{
    public int Id { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal UnitCommission { get; set; }

    public decimal TotalPrice { get; set; }

    public decimal TotalCommission { get; set; }

    public int OrderId { get; set; }

    public string OrderRef { get; set; } = string.Empty;

    public int ProductId { get; set; }

    public string ProductName { get; set; } = string.Empty;
}

