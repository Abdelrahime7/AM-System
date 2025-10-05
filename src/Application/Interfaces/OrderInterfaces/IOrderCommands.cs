

using Application.Common.Models;
using Application.Orders.DTOs;
using Application.Orders.DTOs.Session;
using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.OrderInterfaces
{
    public interface IOrderCommands
    {
        Task<Result<int>> CreatOrderAsync(CreatOrderSession CreatOrdersession);
        Task<Result<bool>> DeleteOrderAsync(int ID);
        Task<Result<bool>> UpdateOrderAsync(UpdateOrderSession request);
        Task<Result<bool>> AssignOrderToDelivery(Order order);



    }
}
