using Application.AffiliateBalance.DTOs;
using Application.AffiliatesBalance.DTOs;
using Application.AuditsLog.DTOs;
using Application.Interfaces.Common.Mappers;


namespace Application.AffiliatesBalance.Mapper;

public class AuditLogMapper : IEntityMapper<Domain.Entities.AffiliateBalance, CreatetAuditLogRequest,
    UpdateAuditLogRequest, AuditLogResponse>
{
  

  

    public AuditLogResponse ToResponse(Domain.Entities.AffiliateBalance entity)
    {
        throw new NotImplementedException();
    }


    public void ToUpdateEntity(Domain.Entities.AffiliateBalance entity, UpdateAuditLogRequest dto)
    {
        throw new NotImplementedException();
    }

    Domain.Entities.AffiliateBalance IEntityMapper<Domain.Entities.AffiliateBalance, CreatetAuditLogRequest, UpdateAuditLogRequest, AuditLogResponse>.ToEntity(CreatetAuditLogRequest dto)
    {
        throw new NotImplementedException();
    }
}