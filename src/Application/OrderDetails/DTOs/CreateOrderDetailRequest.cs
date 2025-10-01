
namespace Application.OrderDetails.DTOs;

public class CreateOrderDetailRequest
{
    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal UnitCommission { get; set; }

    public decimal TotalPrice { get; set; }

    public decimal TotalCommission { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }
}
