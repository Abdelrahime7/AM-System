using Application.AuditsLog.DTOs;
using Application.Common.Models;
using Application.Interfaces.AuditLogInterfaces;

namespace Application.AuditsLog.Features.Commands;

public partial class AuditLogCommands : IAuditLogCommands
{
    public Task<Result<int>> CreatAuditLogAsync(CreateAuditLogRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> DeleteAuditLogAsync(int ID)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> UpdateAuditLogAsync(UpdateAuditLogRequest request)
    {
        throw new NotImplementedException();
    }
}
