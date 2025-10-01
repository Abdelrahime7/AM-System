using Application.Common.Models;
using Application.Customers.DTOs;
using Application.Delivery.DTOs;
using Application.Interfaces.ProductImageInterfaces;
using Application.ProductImages.DTOs;

namespace Application.ProductImages.Features.Queries;

public partial class ProductImageQueries : IProductImageQueries
{
    public Task<Result<IEnumerable<ProductImageResponse>>> GetAllProductImagesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Result<ProductImageResponse>> GetProductImageByIDAsync(int id)
    {
        throw new NotImplementedException();
    }
}