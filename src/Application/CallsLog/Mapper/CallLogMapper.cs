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
        return new CallLog
        {
            CustomerPhone = dto.CustomerPhone,

            CallResult = dto.CallResult,
            CalledAt = dto.CalledAt,
            AgentId = dto.AgentId,
            OrderId = dto.OrderId,
        };
    }

    public CallLogrResponse ToResponse(CallLog entity)
    {
        return new CallLogrResponse
        {
            Id= entity.Id,
            OrderId= entity.OrderId,
            AgentId= entity.AgentId,
            CustomerPhone = entity.CustomerPhone,
            CallResult = entity.CallResult.ToString(),
            CalledAt = entity.CalledAt,
            OrderReference = entity.Order.OrderRef,
            AgentName = entity.Agent.FullName

        };
    }

    public void ToUpdateEntity(CallLog entity, UpdateCallLogRequest dto)
    {
        entity.CustomerPhone = dto.CustomerPhone ?? entity.CustomerPhone;
        entity.CallResult = dto.CallResult ?? entity.CallResult;
        entity.CalledAt = dto.CalledAt ?? entity.CalledAt;
        entity.OrderId = dto.OrderId ?? entity.OrderId;
        entity.AgentId = dto.AgentId ?? entity.AgentId;
    }
}