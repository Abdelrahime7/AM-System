using Application.Common.Models;
using Application.Customers.DTOs;
using Application.Products.DTOs;

namespace Application.Products.Features.Queries;

public partial class ProductQueries
{
    public async Task<Result<ProductResponse>> GetByIdAsync(int id)
    {
        try
        {
            var product = await _repository.GetByIdAsync(id);
            if(product == null)
                return Result<ProductResponse>.Failure("No Product Found");

            var response = _mapper.ToResponse(product);
            return Result<ProductResponse>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<ProductResponse>.Failure($"failed to fetch product: {ex.Message}");
        }
    }
}