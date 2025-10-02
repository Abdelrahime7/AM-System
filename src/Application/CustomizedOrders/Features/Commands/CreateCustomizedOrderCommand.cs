using Application.Common.Models;
using Application.CustomizedOrders.DTOs;
using Application.Delivery.DTOs;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.CustomizedOrderInterfaces;
using Application.Interfaces.DeliveryInterfaces;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.CustomizedOrders.Features.Commands;

public partial class CustomizedOrderCommands : ICustomizedOrderCommands
{
    public Task<Result<int>> CreatCustomizedOrderAsync(CreateCustomizedOrderRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> DeleteCustomizedOrderAsync(int ID)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> UpdateCustomizedOrderAsync(UpdateCustomizedOrderRequest request)
    {
        throw new NotImplementedException();
    }
}
