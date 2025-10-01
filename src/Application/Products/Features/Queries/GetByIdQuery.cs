using Application.Common.Models;
using Application.Customers.DTOs;
using Application.Products.DTOs;

namespace Application.Products.Features.Queries;

public partial class ProductQueries
{
    public async Task<Result<AffiliateBalanceResponse>> GetByIdAsync(int id)
    {
        try
        {
            var product = await _repository.GetByIdAsync(id);
            if(product == null)
                return Result<AffiliateBalanceResponse>.Failure("No Product Found");

            var response = _mapper.ToResponse(product);
            return Result<AffiliateBalanceResponse>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<AffiliateBalanceResponse>.Failure($"failed to fetch product: {ex.Message}");
        }
    }
}