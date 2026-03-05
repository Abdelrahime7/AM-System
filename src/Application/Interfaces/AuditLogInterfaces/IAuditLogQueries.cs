

using Application.Common.Models;

using Application.AuditsLog.DTOs;

namespace Application.Interfaces.AuditLogInterfaces
{
    public interface IAuditLogQueries
    {
        Task<Result<IEnumerable<AuditLogResponse>>> GetAllAuditLogsAsync();
        Task<Result<AuditLogResponse>> GetAuditLogByIDAsync(int id);
       

    }
}
