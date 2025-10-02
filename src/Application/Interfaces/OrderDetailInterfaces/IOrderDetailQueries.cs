

using Application.Common.Models;
using Application.OrderDetails.DTOs;
using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.OrderDetailInterfaces
{
    public interface IOrderDetailQueries
    {
        Task<Result<IEnumerable<OrderDetailResponse>>> GetAllOrderDetailsAsync();
        Task<Result<OrderDetailResponse>> GetOrderDetailByIDAsync(int id);
      

    }
}
