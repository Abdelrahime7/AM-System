using Application.Common.Models;
using Application.ProductImages.DTOs;

namespace Application.Interfaces.ProductImagesInterfaces;

public interface IProductImageQueries
{
    Task<Result<IEnumerable<ProductImageResponse>>> GetAllAsync();
    Task<Result<ProductImageResponse>> GetByIdAsync(int id);
    Task<Result<IEnumerable<ProductImageResponse>>> GetByProductIdAsync(int productId);
    Task<Result<IEnumerable<ProductImageResponse>>> GetByCustomizedOrderIdAsync(int customizedOrderId);
    Task<Result<ProductImageResponse>> GetPrimaryImageByProductIdAsync(int productId);
    Task<Result<ProductImageResponse>> GetPrimaryImageByCustomizedOrderIdAsync(int customizedOrderId);
}