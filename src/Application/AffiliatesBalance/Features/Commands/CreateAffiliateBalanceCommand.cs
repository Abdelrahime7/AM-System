using Application.Common.Models;
using Application.AffiliatesBalance.DTOs;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.AffiliateBalanceInterfaces;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.AffiliatesBalance.Features.Commands;

public partial class AffiliateBalanceCommands(
    IAffiliateBalanceRepository repository,
    IEntityMapper<AffiliateBalance, CreateAffiliateBalanceRequest, UpdateAffiliateBalanceRequest, AffiliateBalanceResponse> mapper)
    : IAffiliateBalanceCommands
{
    private readonly IAffiliateBalanceRepository _repository = repository;
    private readonly IEntityMapper<AffiliateBalance, CreateAffiliateBalanceRequest, UpdateAffiliateBalanceRequest, AffiliateBalanceResponse> _mapper = mapper;
    
    public async Task<Result<int>> CreateAffiliateBalanceAsync(CreateAffiliateBalanceRequest request)
    {
        try
        {
            var affiliateBalance = _mapper.ToEntity(request);
            await _repository.AddAsync(affiliateBalance);
            return Result<int>.Success(affiliateBalance.Id);
        }
        catch (Exception ex)
        {
            return Result<int>.Failure($"Error creating affiliate balance: {ex.Message}");
        }
    }
}