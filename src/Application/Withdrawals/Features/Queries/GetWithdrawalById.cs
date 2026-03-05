using Application.Common.Models;
using Application.Withdrawals.DTOs;

namespace Application.Withdrawals.Features.Queries;

public partial class WithdrawalQueries
{
    public async Task<Result<WithdrawalResponse>> GetWithdrawalByIdAsync(int id)
    {
        try
        {
            var withdrawal = await _repository.GetByIdAsync(id);
            if (withdrawal == null)
                return Result<WithdrawalResponse>.Failure("No Withdrawal Found");

            var response = _mapper.ToResponse(withdrawal);
            return Result<WithdrawalResponse>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<WithdrawalResponse>.Failure($"failed to fetch withdrawal: {ex.Message}");
        }
    }
}