using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class ProductImageRepository(AppDbContext context) : GenericRepository<ProductImage>(context), IProductImageRepository
{
    
}
