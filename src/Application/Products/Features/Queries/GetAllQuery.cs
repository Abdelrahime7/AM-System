using Application.Common.Models;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.ProductInterfaces;
using Application.Interfaces.Repositories;
using Application.Products.DTOs;
using Domain.Entities;

namespace Application.Products.Features.Queries;

public partial class ProductQueries(
    IProductRepository repository,
    IEntityMapper<Product, CreatetAffiliateBalanceRequest, UpdateAffiliateBalanceRequest, AffiliateBalanceResponse> mapper)
    : IProductQueries
{
    private readonly IProductRepository _repository = repository;

    private readonly IEntityMapper<Product, CreatetAffiliateBalanceRequest, UpdateAffiliateBalanceRequest, AffiliateBalanceResponse> _mapper =
        mapper;

    public async Task<Result<IEnumerable<AffiliateBalanceResponse>>> GetAllAsync()
    {
        try
        {
            var products = await _repository.GetAllAsync();
            if (!products.Any())
                return Result<IEnumerable<AffiliateBalanceResponse>>.Failure("No Products Found");

            var response = products.ToList().Select(c => _mapper.ToResponse(c));
            return Result<IEnumerable<AffiliateBalanceResponse>>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<AffiliateBalanceResponse>>.Failure($"failed to fetch products: {ex.Message}");
        }
    }
}