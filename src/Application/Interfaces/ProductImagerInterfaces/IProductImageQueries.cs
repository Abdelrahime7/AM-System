

using Application.Common.Models;
using Application.ProductImages.DTOs;
using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.ProductImageInterfaces
{
    public interface IProductImageQueries
    {
        Task<Result<IEnumerable<ProductImageResponse>>> GetAllProductImagesAsync();
        Task<Result<ProductImageResponse>> GetProductImageByIDAsync(int id);
       ;

    }
}
