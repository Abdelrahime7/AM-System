using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class OrderRepository(AppDbContext context) : GenericRepository<Order>(context), IOrderRepository
{

    public override async Task<Order?> GetByIdAsync(int id)
    {
        return await context.Orders
        .Include(o => o.Customer)
        .Include(o=>o.Affiliate)
        .Include(o=>o.Driver)
        .Include(o=>o.Reviewer)
        .Include(o => o.DeliveryCompany)
        .Include(o=>o.Customizations)
        .Include(o => o.OrderDetails)

        .FirstOrDefaultAsync(o => o.Id == id);
    }
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



    public async Task<int> CountPendingAsync(CancellationToken cancellationToken = default)
    {
        return await context.Orders.CountAsync(o => o.Status == OrderStatus.Pending);
    }

    public async Task<decimal> TotalSalesAsync(CancellationToken cancellationToken = default)
    {
        return await context.Orders
    .Where(o => o.Status == OrderStatus.Delivered)
    .Select(o => o.IsCustomized
        ? o.Customizations.Sum(c => c.TotalPrice)
        : o.OrderDetails.Sum(od => od.TotalPrice))
    .SumAsync();

    }
}

