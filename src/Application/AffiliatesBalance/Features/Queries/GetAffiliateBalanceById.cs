using Application.AffiliatesBalance.DTOs;
using Application.Common.Models;

using Application.Interfaces.AffiliateBalanceInterfaces;


namespace Application.AffiliatesBalance.Features.Queries;

public partial class AffiliateBalanceQueries : IAffiliateBalanceQueries
{
    public Task<Result<AffiliateBalanceResponse>> GetAffiliateBalanceByIDAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<AffiliateBalanceResponse>>> GetAllAffiliateBalancesAsync()
    {
        throw new NotImplementedException();
    }
}