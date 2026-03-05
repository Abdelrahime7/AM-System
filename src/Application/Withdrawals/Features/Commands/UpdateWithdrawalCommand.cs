using Application.Common.Models;
using Application.Withdrawals.DTOs;

namespace Application.Withdrawals.Features.Commands;

public partial class WithdrawalCommands
{
    public async Task<Result<bool>> UpdateWithdrawalAsync(UpdateWithdrawalRequest request)
    {
        try
        {
            var withdrawal = await _repository.GetByIdAsync(request.Id);
            if (withdrawal == null)
                return Result<bool>.Failure("Withdrawal Not Found");

            _mapper.ToUpdateEntity(withdrawal, request);
            _repository.Update(withdrawal);
            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure($"failed to update withdrawal: {ex.Message}");
        }
    }
}