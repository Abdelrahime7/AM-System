using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class OrderDetailRepository(AppDbContext context) : GenericRepository<OrderDetail>(context), IOrderDetailRepository
{
   
}
