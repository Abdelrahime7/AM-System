using Domain.Entities;
using System;

namespace Application.Interfaces.Repositories;

public interface IOrderRepository : IGenericRepository<Order>
{

    Task CommitAsync(CancellationToken cancellationToken = default);

}