using Application.AffiliatesBalance.DTOs;
using Application.Common.Models;

namespace Application.Interfaces.AffiliateBalanceInterfaces
{
    public interface IAffiliateBalanceCommands
    {
        Task<Result<int>> CreateAffiliateBalanceAsync(CreateAffiliateBalanceRequest request);
        Task<Result<bool>> DeleteAffiliateBalanceAsync(int id);
        Task<Result<bool>> UpdateAffiliateBalanceAsync(UpdateAffiliateBalanceRequest request);

    }
}
