using Application.Customers.DTOs;
using Application.CustomizedOrders.DTOs;
using Application.OrderDetails.DTOs;
using Domain.Enums;

namespace Application.Orders.DTOs.Session;

public class CreatOrderSession

{
   public CreateCustomerRequest?  Customer { get; set; }
   public ChangeOrderStatus? Order { get; set; }
   public List<CreateCustomizedOrderRequest?> Customizations { get; set; } = [];
   public List<CreateOrderDetailRequest?> OrderDetails { get; set; } = [];
      
}
