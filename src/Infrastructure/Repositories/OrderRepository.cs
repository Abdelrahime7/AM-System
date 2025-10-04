using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class OrderRepository(AppDbContext context) : GenericRepository<Order>(context), IOrderRepository
{
    public override async Task AddAsync(Order entity)
    {
        await context.Orders.AddAsync(entity);
    }
    public override void Update(Order order)
    {
     _context.Orders.Update(order);
    }
    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }

}

