using Application.CallsLog.DTOs;
using Application.Common.Models;
using Application.Customers.DTOs;
using Application.Delivery.DTOs;
using Application.Interfaces.CallLogInterfaces;

namespace Application.CallsLog.Features.Queries;

public partial class CallLogQueries : ICallLogQueries
{
    public Task<Result<IEnumerable<CallLogrResponse>>> GetAllCallLogsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Result<CallLogrResponse>> GetCallLogByIDAsync(int id)
    {
        throw new NotImplementedException();
    }
}