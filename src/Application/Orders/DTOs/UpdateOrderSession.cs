using Application.Customers.DTOs;
using Application.CustomizedOrders.DTOs;
using Application.OrderDetails.DTOs;

namespace Application.Orders.DTOs;

public class UpdateOrderSession

{
   public UpdateOrderRequest? Order { get; set; }
   public List<UpdateCustomizedOrderRequest?> Customizations { get; set; } = [];
   public List<UpdateOrderDetailRequest?> OrderDetails { get; set; } = [];
      
}
