using Application.CallsLog.DTOs;
using Application.Common.Models;
using Application.Customers.DTOs;
using Application.Delivery.DTOs;

namespace Application.CallsLog.Features.Commands;

public partial class CallLogCommands
{
    public async Task<Result<bool>> UpdateCallLogAsync(UpdateCallLogRequest request)
    {
        try
        {
            var callLog = await _repository.GetByIdAsync(request.Id);
            if (callLog == null)
                return Result<bool>.Failure("call Log Not Found");

            _mapper.ToUpdateEntity(callLog, request);
            _repository.Update(callLog);
            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure($"failed to update call Log: {ex.Message}");
        }
    }

}
