

using Application.Common.Models;
using Application.Orders.DTOs;
using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.OrderInterfaces
{
    public interface IOrderCommands
    {
        Task<Result<int>> CreatOrderAsync(CreateOrderRequest request);
        Task<Result<bool>> DeleteOrderAsync(int ID);
        Task<Result<bool>> UpdateOrderAsync(UpdateOrderRequest request);
       

    }
}
