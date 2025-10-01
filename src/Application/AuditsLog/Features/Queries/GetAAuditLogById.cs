using Application.AffiliatesBalance.DTOs;
using Application.AuditsLog.DTOs;
using Application.Common.Models;
using Application.Customers.DTOs;
using Application.Delivery.DTOs;
using Application.Interfaces.AffiliateBalanceInterfaces;
using Application.Interfaces.AuditLogInterfaces;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.CustomerInterfaces;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.AuditsLog.Features.Queries;

public partial class AuditLogQueries : IAuditLogQueries
{
    public Task<Result<IEnumerable<AuditLogResponse>>> GetAllAuditLogsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Result<AuditLogResponse>> GetAuditLogByIDAsync(int id)
    {
        throw new NotImplementedException();
    }
}