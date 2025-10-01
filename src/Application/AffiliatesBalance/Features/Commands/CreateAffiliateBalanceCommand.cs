using Application.AffiliatesBalance.DTOs;
using Application.Common.Models;
using Application.Interfaces.AffiliateBalanceInterfaces;

namespace Application.AffiliatesBalance.Features.Commands;

public partial class AffiliatesBalanceCommands : IAffiliateBalanceCommands
{
    public Task<Result<int>> CreatAffiliateBalanceAsync(CreatetAffiliateBalanceRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> DeleteAffiliateBalanceAsync(int ID)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> UpdateAffiliateBalanceAsync(UpdateAffiliateBalanceRequest request)
    {
        throw new NotImplementedException();
    }
}
