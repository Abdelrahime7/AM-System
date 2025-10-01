using Application.AffiliatesBalance.DTOs;
using Application.Common.Models;
using Application.Customers.DTOs;
using Application.Delivery.DTOs;
using Application.Interfaces.AffiliateBalanceInterfaces;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.CustomerInterfaces;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.AuditsLog.Features.Queries;

public partial class AuditLogQueries : IAffiliateBalanceQueries
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