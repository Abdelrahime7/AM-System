

using Application.Common.Models;
using Application.Orders.DTOs;
using Application.Orders.DTOs.Session;
using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.OrderInterfaces
{
    public interface IOrderQueries
    {
        Task<Result<IEnumerable<ResponseSession>>> GetAllOrdersAsync();
        Task<Result<ResponseSession>> GetOrderByIDAsync(int id);
      

    }
}
