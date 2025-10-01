using Application.AuditsLog.DTOs;
using Application.CustomizedOrders.DTOs;
using Application.Interfaces.Common.Mappers;
using Domain.Entities;


namespace Application.AuditsLog.Mapper;

public class AuditLogMapper : IEntityMapper<AuditLog, CreateCallLogRequest,
    UpdateCallLogRequest, CallLogResponse>
{
    public AuditLog ToEntity(CreateCallLogRequest dto)
    {
        throw new NotImplementedException();
    }

  

    public CustomizedOrderResponse ToResponse(AuditLog entity)
    {
        throw new NotImplementedException();
    }

   

    public void ToUpdateEntity(AuditLog entity, CustomizedOrders.DTOs.UpdateCustomizedOrderRequest dto)
    {
        throw new NotImplementedException();
    }

  
}