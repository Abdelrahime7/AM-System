using Application.Common.Models;
using Application.Products.DTOs;

namespace Application.Interfaces.ProductInterfaces;

public interface IProductQueries
{
    Task<Result<IEnumerable<AffiliateBalanceResponse>>> GetAllAsync();
    Task<Result<AffiliateBalanceResponse>> GetByIdAsync(int id);
    Task<Result<AffiliateBalanceResponse>> GetByNameAsync(string name);
}