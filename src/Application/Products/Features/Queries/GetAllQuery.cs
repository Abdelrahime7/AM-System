using Application.Common.Models;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.ProductInterfaces;
using Application.Interfaces.Repositories;
using Application.Products.DTOs;
using Domain.Entities;

namespace Application.Products.Features.Queries;

public partial class ProductQueries(
    IProductRepository repository,
    IEntityMapper<Product, CreateProductRequest, UpdateProductRequest, ProductResponse> mapper)
    : IProductQueries
{
    private readonly IProductRepository _repository = repository;

    private readonly IEntityMapper<Product, CreateProductRequest, UpdateProductRequest, ProductResponse> _mapper =
        mapper;

    public async Task<Result<IEnumerable<ProductResponse>>> GetAllAsync()
    {
        try
        {
            var products = await _repository.GetAllAsync();
            if (!products.Any())
                return Result<IEnumerable<ProductResponse>>.Failure("No Products Found");

            var response = products.ToList().Select(c => _mapper.ToResponse(c));
            return Result<IEnumerable<ProductResponse>>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<ProductResponse>>.Failure($"failed to fetch products: {ex.Message}");
        }
    }
}