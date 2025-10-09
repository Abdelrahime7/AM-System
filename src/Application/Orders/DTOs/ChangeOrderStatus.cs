using Domain.Enums;

namespace Application.Orders.DTOs;

public class ChangeOrderStatus
{
  public int Id { get; set; }
 public OrderStatus Status { get; set; }

  
}
