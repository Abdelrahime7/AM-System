using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<Product?> GetByNameAsync(string name);
    Task<string> GetRecentProductAsync();
}