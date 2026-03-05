using Application.Common.Models;
using Application.ProductImages.DTOs;

namespace Application.Interfaces.ProductImagesInterfaces;

public interface IProductImageCommands
{
    Task<Result<int>> CreateProductImageAsync(CreateProductImageRequest request);
    Task<Result<bool>> DeleteProductImageAsync(int id);
    Task<Result<bool>> UpdateProductImageAsync(UpdateProductImageRequest request);
}