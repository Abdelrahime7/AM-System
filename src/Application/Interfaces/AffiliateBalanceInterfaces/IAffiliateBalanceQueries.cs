using Application.Common.Models;
using Application.AffiliatesBalance.DTOs;

namespace Application.Interfaces.AffiliateBalanceInterfaces
{
    public interface IAffiliateBalanceQueries
    {
        Task<Result<IEnumerable<AffiliateBalanceResponse>>> GetAllAffiliateBalancesAsync();
        Task<Result<AffiliateBalanceResponse>> GetAffiliateBalanceByIdAsync(int id);

    }
}
