using Application.AuditsLog.DTOs;
using Application.CustomizedOrders.DTOs;
using Application.Interfaces.Common.Mappers;
using Domain.Entities;


namespace Application.AuditsLog.Mapper;

public class AuditLogMapper : IEntityMapper<AuditLog, CreateAuditLogRequest,
    UpdateAuditLogRequest, AuditLogResponse>
{
    public AuditLog ToEntity(CreateAuditLogRequest dto)
    {
        throw new NotImplementedException();
    }

    public void ToUpdateEntity(AuditLog entity, UpdateAuditLogRequest dto)
    {
        throw new NotImplementedException();
    }

    AuditLogResponse IEntityMapper<AuditLog, CreateAuditLogRequest, UpdateAuditLogRequest, AuditLogResponse>.ToResponse(AuditLog entity)
    {
        throw new NotImplementedException();
    }

}