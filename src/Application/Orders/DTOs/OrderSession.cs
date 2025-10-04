using Application.Customers.DTOs;
using Application.CustomizedOrders.DTOs;
using Application.OrderDetails.DTOs;
using Domain.Enums;

namespace Application.Orders.DTOs;

public class OrderSession

{
   public CreateOrderRequest? Order { get; set; }
   public List<CreateCustomizedOrderRequest?> Customizations { get; set; } = [];
   public List<CreateOrderDetailRequest?> OrderDetails { get; set; } = [];
      
}
