using Application.Common.Models;

namespace Application.Withdrawals.Features.Commands;

public partial class WithdrawalCommands
{
    public async Task<Result<bool>> DeleteWithdrawalAsync(int id)
    {
        try
        {
            var withdrawal = await _repository.GetByIdAsync(id);
            if (withdrawal == null)
                return Result<bool>.Failure("Withdrawal Not Found");

            _repository.Delete(withdrawal);
            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure($"failed to delete withdrawal: {ex.Message}");
        }
    }
}
