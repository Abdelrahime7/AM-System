

using Application.Common.Models;
using Application.CustomizedOrders.DTOs;
using Application.OrderDetails.DTOs;
using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.OrderDetailInterfaces
{
    public interface IOrderDetailCommands
    {
        Task<Result<int>> CreatOrderDetailAsync(CreateOrderDetailRequest request);
        Task<Result> AddRangeAsync(List<CreateOrderDetailRequest> request);

        Task<Result<bool>> DeleteOrderDetailAsync(int ID);
        Task<Result<bool>> UpdateOrderDetailAsync(UpdateOrderDetailRequest request);

    }
}
