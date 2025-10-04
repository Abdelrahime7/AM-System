using Application.Customers.DTOs;
using Application.CustomizedOrders.DTOs;
using Application.OrderDetails.DTOs;
using Domain.Enums;

namespace Application.Orders.DTOs;

public class OrderSession

{
   public CreateCustomerRequest?  Customer { get; set; }
   public CreateOrderRequest? Order { get; set; }
   public List<CreateCustomizedOrderRequest?> Customization { get; set; } = [];
   public List<CreateOrderDetailRequest?> OrderDetail { get; set; } = [];
      
}
