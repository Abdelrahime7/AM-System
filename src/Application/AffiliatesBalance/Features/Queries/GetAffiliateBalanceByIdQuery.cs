using Application.AffiliatesBalance.DTOs;
using Application.Common.Models;
using Application.Interfaces.AffiliateBalanceInterfaces;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.AffiliatesBalance.Features.Queries;

public partial class AffiliateBalanceQueries(
    IAffiliateBalanceRepository repository,
    IEntityMapper<AffiliateBalance, CreateAffiliateBalanceRequest, UpdateAffiliateBalanceRequest, AffiliateBalanceResponse> mapper)
    : IAffiliateBalanceQueries
{
    private readonly IAffiliateBalanceRepository _repository = repository;
    private readonly IEntityMapper<AffiliateBalance, CreateAffiliateBalanceRequest, UpdateAffiliateBalanceRequest, AffiliateBalanceResponse> _mapper = mapper;
    
    public async Task<Result<AffiliateBalanceResponse>> GetAffiliateBalanceByIdAsync(int id)
    {
        try
        {
            var affiliateBalance = await _repository.GetByIdAsync(id);
            if(affiliateBalance == null)
                return Result<AffiliateBalanceResponse>.Failure("No Affiliate Balance Found");

            var response = _mapper.ToResponse(affiliateBalance);
            return Result<AffiliateBalanceResponse>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<AffiliateBalanceResponse>.Failure($"failed to fetch Affiliate Balance: {ex.Message}");
        }
    }

}