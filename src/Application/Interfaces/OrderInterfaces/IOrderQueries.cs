

using Application.Common.Models;
using Application.Orders.DTOs;
using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.OrderInterfaces
{
    public interface IOrderQueries
    {
        Task<Result<IEnumerable<OrderResponse>>> GetAllOrdersAsync();
        Task<Result<OrderResponse>> GetOrderByIDAsync(int id);
      

    }
}
