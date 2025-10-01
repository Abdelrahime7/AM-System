

using Application.Common.Models;
using Application.ProductImages.DTOs;
using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.ProductImageInterfaces
{
    public interface IProductImageCommands
    {
        Task<Result<int>> CreatProductImageAsync(CreateProductImageRequest request);
        Task<Result<bool>> DeleteProductImageAsync(int ID);
        Task<Result<bool>> UpdateProductImageAsync(UpdateProductImageRequest request);
      

    }
}
