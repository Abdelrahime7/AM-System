

using Application.Common.Models;
using Application.CustomizedOrders.DTOs;
using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.CustomizedOrderInterfaces
{
    public interface ICustomizedOrderQueries
    {
        Task<Result<IEnumerable<CustomizedOrderResponse>>> GetAllCustomizedOrdersAsync();
        Task<Result<CustomizedOrderResponse>> GetCustomizedOrderByIDAsync(int id);
        

    }
}
