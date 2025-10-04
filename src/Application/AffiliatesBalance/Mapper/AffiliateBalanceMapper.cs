using Application.AffiliatesBalance.DTOs;
using Application.Interfaces.Common.Mappers;
using Domain.Entities;

namespace Application.AffiliatesBalance.Mapper;

public class AffiliateBalanceMapper : IEntityMapper<AffiliateBalance, CreateAffiliateBalanceRequest, UpdateAffiliateBalanceRequest, AffiliateBalanceResponse>
{
    public AffiliateBalance ToEntity(CreateAffiliateBalanceRequest dto)
    {
        return new AffiliateBalance
        {
            Amount = dto.Amount,
            AffiliateId = dto.AffiliateId
        };
    }

    public AffiliateBalanceResponse ToResponse(AffiliateBalance entity)
    {
        return new AffiliateBalanceResponse
        {
            Id = entity.Id,
            Amount = entity.Amount,
            AffiliateId = entity.AffiliateId,
            AffiliateName = entity.Affiliate?.FullName
        };
    }

    public void ToUpdateEntity(AffiliateBalance entity, UpdateAffiliateBalanceRequest dto)
    {
        entity.Amount = dto.Amount ?? entity.Amount;
    }
}