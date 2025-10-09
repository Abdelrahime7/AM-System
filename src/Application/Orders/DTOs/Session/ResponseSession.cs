using Application.Customers.DTOs;
using Application.CustomizedOrders.DTOs;
using Application.OrderDetails.DTOs;
using Domain.Enums;

namespace Application.Orders.DTOs.Session;

public class ResponseSession

{
   public CustomerResponse?  Customer { get; set; }
   public OrderResponse? Order { get; set; }
   public List<CustomizedOrderResponse?> Customizations { get; set; } = [];
   public List<OrderDetailResponse?> OrderDetails { get; set; } = [];
      
}
