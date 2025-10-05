using Application.Common.Models;
using Application.Withdrawals.DTOs;

namespace Application.Interfaces.WithdrawalInterfaces;

public interface IWithdrawalCommands
{
    Task<Result<int>> CreateWithdrawalAsync(CreateWithdrawalRequest request);
    Task<Result<bool>> DeleteWithdrawalAsync(int id);
    Task<Result<bool>> UpdateWithdrawalAsync(UpdateWithdrawalRequest request);
}