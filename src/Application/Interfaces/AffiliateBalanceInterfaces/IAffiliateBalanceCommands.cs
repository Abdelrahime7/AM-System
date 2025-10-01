

using Application.AffiliatesBalance.DTOs;
using Application.Common.Models;

namespace Application.Interfaces.AffiliateBalanceInterfaces
{
    public interface IAffiliateBalanceCommands
    {
        Task<Result<int>> CreatAffiliateBalanceAsync(CreatetAffiliateBalanceRequest request);
        Task<Result<bool>> DeleteAffiliateBalanceAsync(int ID);
        Task<Result<bool>> UpdateAffiliateBalanceAsync(UpdateAffiliateBalanceRequest request);

    }
}
