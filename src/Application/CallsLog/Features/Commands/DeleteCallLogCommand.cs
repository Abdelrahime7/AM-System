using Application.Common.Models;

namespace Application.CallsLog.Features.Commands;

public partial class CallLogCommands
{
    public async Task<Result<bool>> DeleteCallLogAsync(int id)
    {
        try
        {
            var callLog = await _repository.GetByIdAsync(id);
            if (callLog == null)
                return Result<bool>.Failure("call Log Not Found");
            else
                _repository.Delete(callLog);

            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure($"failed to delete call Log: {ex.Message}");
        }
    }
}