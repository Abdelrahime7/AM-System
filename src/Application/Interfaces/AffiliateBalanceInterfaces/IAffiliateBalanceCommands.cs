

using Application.Common.Models;
using Application.AffiliateBalances.DTOs;
using Domain.Enums;

namespace Application.Interfaces.AffiliateBalanceInterfaces
{
    public interface IAffiliateBalanceCommands
    {
        Task<Result<int>> CreatAffiliateBalanceAsync(CreateAffiliateBalanceRequest request);
        Task<Result<bool>> DeleteAffiliateBalanceAsync(int ID);
        Task<Result<bool>> UpdateAffiliateBalanceAsync(UpdateAffiliateBalanceRequest request);
        Task<Result<bool>> ChangeAffiliateBalanceStatusAsync(UpdateAffiliateBalanceRequest request,AffiliateBalanceStatus status);

    }
}
