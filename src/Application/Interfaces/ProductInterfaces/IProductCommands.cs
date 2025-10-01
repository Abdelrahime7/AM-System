using Application.Common.Models;
using Application.Products.DTOs;

namespace Application.Interfaces.ProductInterfaces;

public interface IProductCommands
{
    Task<Result<int>> CreateProductAsync(CreatetAffiliateBalanceRequest request);
    Task<Result<bool>> DeleteProductAsync(int id);
    Task<Result<bool>> UpdateProductAsync(UpdateAffiliateBalanceRequest request);
}