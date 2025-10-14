using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IProductImageRepository : IGenericRepository<ProductImage>
{
    Task<IEnumerable<ProductImage>> GetByProductIdAsync(int productId);
    Task<IEnumerable<ProductImage>> GetByCustomizedOrderIdAsync(int customizedOrderId);
    Task<ProductImage?> GetPrimaryImageByProductIdAsync(int productId);
    Task<ProductImage?> GetPrimaryImageByCustomizedOrderIdAsync(int customizedOrderId);
    Task<bool> SetPrimaryImageAsync(int imageId);
    Task<bool> HasAnyPrimaryImageForProductAsync(int productId);
    Task<bool> HasAnyPrimaryImageForCustomizedOrderAsync(int customizedOrderId);
}