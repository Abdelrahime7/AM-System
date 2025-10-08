using Domain.Entities;
using Domain.Enums;

namespace Application.CustomizedOrders.DTOs;

public record CreateCustomizedOrderRequest
{
   
    
    public required string  Name { get; set; } = string.Empty;

    public required string Description { get; set; }

    public required string Dimensions { get; set; } = string.Empty;

    public required CustomizedOrderStatus Status { get; set; }

//  public required decimal TotalPrice { get; set; }

  //public required decimal CommissionAmount { get; set; }

    public int? OrderId { get; set; }

 // public Order Order { get; set; }
    public required List<string> ImageUrls { get; set; } = new();
}