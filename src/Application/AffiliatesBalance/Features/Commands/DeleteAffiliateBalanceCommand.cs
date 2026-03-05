using Application.Common.Models;

namespace Application.AffiliatesBalance.Features.Commands;

public partial class AffiliateBalanceCommands
{
    public async Task<Result<bool>> DeleteAffiliateBalanceAsync(int id)
    {
        try
        {
            var affiliateBalance = await _repository.GetByIdAsync(id);
            if (affiliateBalance == null)
                return Result<bool>.Failure("Affiliate Balance Not Found");
            
            _repository.Delete(affiliateBalance);
            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure($"failed to delete affiliate balance: {ex.Message}");
        }
    }
}
