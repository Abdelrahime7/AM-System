using Application.Common.Models;
using Application.CustomizedOrders.DTOs;
using Application.Delivery.DTOs;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.DeliveryInterfaces;
using Application.Interfaces.OrderDetailInterfaces;
using Application.Interfaces.Repositories;
using Application.OrderDetails.DTOs;
using Domain.Entities;

namespace Application.OrderDetails.Features.Commands;

public partial class OrderDetailCommands 
{
   

    public Task<Result<int>> CreatOrderDetailAsync(CreateOrderDetailRequest request)
    {
        throw new NotImplementedException();
    }

   

  
}
