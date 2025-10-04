using Application.CustomizedOrders.DTOs;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class CustomizedOrderRepository(AppDbContext context) : GenericRepository<CustomizedOrder>(context), ICustomizedOrderRepository
{
    public async Task AddRangeAsync(List<CustomizedOrder> Customizations)
    {

      await context.CustomizedOrders.AddRangeAsync(Customizations);
    }
}
