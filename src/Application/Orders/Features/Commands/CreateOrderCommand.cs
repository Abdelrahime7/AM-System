using Application.Common.Models;
using Application.Delivery.DTOs;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.DeliveryInterfaces;
using Application.Interfaces.OrderInterfaces;
using Application.Interfaces.Repositories;
using Application.Orders.DTOs;
using Domain.Entities;

namespace Application.Orders.Features.Commands;

public partial class OrderCommands : IOrderCommands
{
    public Task<Result<int>> CreatOrderAsync(OrderSession CreatOrdersession)
    {
        


    }

  

   
}
