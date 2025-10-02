using Application.Common.Models;
using Application.Delivery.DTOs;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.DeliveryInterfaces;
using Application.Interfaces.OrderDetailInterfaces;
using Application.Interfaces.Repositories;
using Application.OrderDetails.DTOs;
using Domain.Entities;

namespace Application.OrderDetails.Features.Commands;

public partial class OrderDetailCommands : IOrderDetailCommands
{
    public Task<Result<int>> CreatOrderDetailAsync(CreateOrderDetailRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> DeleteOrderDetailAsync(int ID)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> UpdateOrderDetailAsync(UpdateOrderDetailRequest request)
    {
        throw new NotImplementedException();
    }
}
