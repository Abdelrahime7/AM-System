
using Application.AffiliatesBalance.DTOs;
using Application.AuditsLog.DTOs;
using Application.Interfaces.Common.Mappers;
using Domain.Entities;


namespace Application.AffiliatesBalance.Mapper;

public class AffiliateBalanceMapper : IEntityMapper<AffiliateBalance, CreatetAffiliateBalanceRequest,
    UpdateAffiliateBalanceRequest, AffiliateBalanceResponse>
{
    public AffiliateBalance ToEntity(CreatetAffiliateBalanceRequest dto)
    {
        throw new NotImplementedException();
    }

    public AffiliateBalanceResponse ToResponse(AffiliateBalance entity)
    {
        throw new NotImplementedException();
    }

    public void ToUpdateEntity(AffiliateBalance entity, UpdateAffiliateBalanceRequest dto)
    {
        throw new NotImplementedException();
    }
}