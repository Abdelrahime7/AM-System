using Application.Common.Models;
using Application.Products.DTOs;

namespace Application.Interfaces.ProductInterfaces;

public interface IProductQueries
{
    Task<Result<IEnumerable<ProductResponse>>> GetAllAsync();
    Task<Result<ProductResponse>> GetByIdAsync(int id);
    Task<Result<ProductResponse>> GetByNameAsync(string name);
}