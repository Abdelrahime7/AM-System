using Application.Common.Models;
using Application.CustomizedOrders.DTOs;
using Application.Delivery.DTOs;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.CustomizedOrderInterfaces;
using Application.Interfaces.DeliveryInterfaces;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.CustomizedOrders.Features.Commands;

public partial class CustomizedOrderCommands

{
    public Task<Result<int>> CreatCustomizedOrderAsync(CreateCustomizedOrderRequest request)
    {
        throw new NotImplementedException();
    }

    

  
}
