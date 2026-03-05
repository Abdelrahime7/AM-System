using Application.Common.Models;
using Application.Withdrawals.DTOs;

namespace Application.Interfaces.WithdrawalInterfaces;

public interface IWithdrawalQueries
{
    Task<Result<IEnumerable<WithdrawalResponse>>> GetAllWithdrawalsAsync();
    Task<Result<WithdrawalResponse>> GetWithdrawalByIdAsync(int id);
}