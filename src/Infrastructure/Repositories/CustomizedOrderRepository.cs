using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class CustomizedOrderRepository(AppDbContext context) : GenericRepository<CustomizedOrder>(context), ICustomizedOrderRepository
{
    
}
