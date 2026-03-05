using Application.AffiliatesBalance.DTOs;
using Application.Common.Models;
using Application.Customers.DTOs;
using Application.Delivery.DTOs;

namespace Application.AffiliatesBalance.Features.Queries;

public partial class AffiliateBalanceQueries
{
    public async Task<Result<IEnumerable<AffiliateBalanceResponse>>> GetAllAffiliateBalancesAsync()
    {
        try
        {
            var affiliateBalances = await _repository.GetAllAsync();
            if(!affiliateBalances.Any())
                return Result<IEnumerable<AffiliateBalanceResponse>>.Failure("No Affiliate Balances Found");

            var response = affiliateBalances.ToList().Select(c => _mapper.ToResponse(c));
            return Result<IEnumerable<AffiliateBalanceResponse>>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<AffiliateBalanceResponse>>.Failure($"failed to fetch Affiliate Balances: {ex.Message}");
        }
    }
}