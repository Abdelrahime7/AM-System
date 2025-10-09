using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IOrderDetailRepository : IGenericRepository<OrderDetail>
{
    Task AddRangeAsync(List<OrderDetail> details);
}