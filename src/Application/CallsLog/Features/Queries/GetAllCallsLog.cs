using Application.CallsLog.DTOs;
using Application.Common.Models;
using Application.Customers.DTOs;
using Application.Delivery.DTOs;
using Application.Interfaces.CallLogInterfaces;

namespace Application.CallsLog.Features.Queries;

public partial class CallLogQueries 
{
    public async Task<Result<IEnumerable<CallLogrResponse>>> GetAllCallLogsAsync()
    {
        try
        {
            var callLogs = await _repository.GetAllAsync();
            if (!callLogs.Any())
                return Result<IEnumerable<CallLogrResponse>>.Failure("No callLogs Found");

            var response = callLogs.ToList().Select(c => _mapper.ToResponse(c));
            return Result<IEnumerable<CallLogrResponse>>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<CallLogrResponse>>.Failure($"failed to fetch callLogs: {ex.Message}");
        }
    }

   
}