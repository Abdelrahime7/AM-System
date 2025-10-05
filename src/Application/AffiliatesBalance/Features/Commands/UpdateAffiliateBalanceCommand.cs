using Application.Common.Models;
using Application.AffiliatesBalance.DTOs;
namespace Application.AffiliatesBalance.Features.Commands;

public partial class AffiliateBalanceCommands
{
    public async Task<Result<bool>> UpdateAffiliateBalanceAsync(UpdateAffiliateBalanceRequest request)
    {
        try
        {
            var affiliateBalance = await _repository.GetByIdAsync(request.Id);
            if (affiliateBalance == null)
                return Result<bool>.Failure("Affiliate Balance Not Found");

            _mapper.ToUpdateEntity(affiliateBalance, request); 
            _repository.Update(affiliateBalance);
            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure($"failed to update affiliate balance: {ex.Message}");
        }
    }
}