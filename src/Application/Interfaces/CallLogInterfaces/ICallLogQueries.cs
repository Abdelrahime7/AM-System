

using Application.Common.Models;
using Application.CallsLog.DTOs;

namespace Application.Interfaces.CallLogInterfaces
{
    public interface ICallLogQueries
    {
        Task<Result<IEnumerable<CallLogrResponse>>> GetAllCallLogsAsync();
        Task<Result<CallLogrResponse>> GetCallLogByIDAsync(int id);
      

    }
}
