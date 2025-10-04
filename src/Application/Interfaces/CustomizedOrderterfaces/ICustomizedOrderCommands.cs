

using Application.Common.Models;
using Application.CustomizedOrders.DTOs;
using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.CustomizedOrderInterfaces
{
    public interface ICustomizedOrderCommands
    {
        Task<Result<int>> CreatCustomizedOrderAsync(CreateCustomizedOrderRequest request);
        Task<Result> AddRangeAsync(List<CreateCustomizedOrderRequest> request);
        Task<Result<bool>> DeleteCustomizedOrderAsync(int ID);
        Task<Result<bool>> UpdateCustomizedOrderAsync(UpdateCustomizedOrderRequest request);
       

    }
}
