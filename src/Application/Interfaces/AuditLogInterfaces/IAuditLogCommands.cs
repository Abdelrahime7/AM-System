

using Application.Common.Models;
using Application.AuditLogs.DTOs;
using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.AuditLogInterfaces
{
    public interface IAuditLogCommands
    {
        Task<Result<int>> CreatAuditLogAsync(CreateAuditLogRequest request);
        Task<Result<bool>> DeleteAuditLogAsync(int ID);
        Task<Result<bool>> UpdateAuditLogAsync(UpdateAuditLogRequest request);
      

    }
}
