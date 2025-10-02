using Application.Common.Models;
using Application.Delivery.DTOs;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.DeliveryInterfaces;
using Application.Interfaces.ProductImageInterfaces;
using Application.Interfaces.Repositories;
using Application.ProductImages.DTOs;
using Domain.Entities;

namespace Application.ProductImages.Features.Commands;

public partial class ProductImageCommands : IProductImageCommands
{
    public Task<Result<int>> CreatProductImageAsync(CreateProductImageRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> DeleteProductImageAsync(int ID)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> UpdateProductImageAsync(UpdateProductImageRequest request)
    {
        throw new NotImplementedException();
    }
}
