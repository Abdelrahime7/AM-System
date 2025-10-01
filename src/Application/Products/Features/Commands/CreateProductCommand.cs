using Application.Common.Models;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.ProductInterfaces;
using Application.Interfaces.Repositories;
using Application.Products.DTOs;
using Domain.Entities;

namespace Application.Products.Features.Commands;

public partial class ProductCommands(
    IProductRepository repository,
    IEntityMapper<Product, CreateProductRequest, UpdateProductRequest, ProductResponse> mapper)
    : IProductCommands
{
    private readonly IProductRepository _repository = repository;
    private readonly IEntityMapper<Product, CreateProductRequest, UpdateProductRequest, ProductResponse> _mapper = mapper;

    public async Task<Result<int>> CreateProductAsync(CreateProductRequest request)
    {
        try
        {
            var product = _mapper.ToEntity(request); 
            await _repository.AddAsync(product);
            return Result<int>.Success(product.Id);
        }
        catch (Exception e)
        {
            return Result<int>.Failure($"Error creating product: {e.Message}");
        }        
    }
    
}