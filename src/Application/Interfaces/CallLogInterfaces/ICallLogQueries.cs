

using Application.Common.Models;
using Application.CallLogs.DTOs;
using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.CallLogInterfaces
{
    public interface ICallLogQueries
    {
        Task<Result<IEnumerable<CallLogResponse>>> GetAllCallLogsAsync();
        Task<Result<CallLogResponse>> GetCallLogByIDAsync(int id);
      

    }
}
