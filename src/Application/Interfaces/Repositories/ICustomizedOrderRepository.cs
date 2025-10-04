using Application.CustomizedOrders.DTOs;
using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface ICustomizedOrderRepository : IGenericRepository<CustomizedOrder>
{
   Task AddRange(List<CustomizedOrder> Customizations);
}