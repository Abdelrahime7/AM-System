

using Application.Common.Models;
using Application.AuditLogs.DTOs;
using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.AuditLogInterfaces
{
    public interface IAuditLogQueries
    {
        Task<Result<IEnumerable<AuditLogResponse>>> GetAllAuditLogsAsync();
        Task<Result<AuditLogResponse>> GetAuditLogByIDAsync(int id);
       

    }
}
