

using Application.Common.Models;
using Application.AffiliateBalances.DTOs;
using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.AffiliateBalanceInterfaces
{
    public interface IAffiliateBalanceQueries
    {
        Task<Result<IEnumerable<AffiliateBalanceResponse>>> GetAllAffiliateBalancesAsync();
        Task<Result<AffiliateBalanceResponse>> GetAffiliateBalanceByIDAsync(int id);
     

    }
}
