using Application.AuditsLog.DTOs;
using Application.CallsLog.DTOs;
using Application.CustomizedOrders.DTOs;
using Application.Interfaces.Common.Mappers;
using Domain.Entities;


namespace Application.CallsLog.Mapper;

public class CallLogMapper : IEntityMapper<CallLog, CreateCallLogRequest,
    UpdateCallLogRequest, CallLogrResponse>
{
    public CallLog ToEntity(CreateCallLogRequest dto)
    {
        throw new NotImplementedException();
    }

    public CallLogrResponse ToResponse(CallLog entity)
    {
        throw new NotImplementedException();
    }

    public void ToUpdateEntity(CallLog entity, UpdateCallLogRequest dto)
    {
        throw new NotImplementedException();
    }
}