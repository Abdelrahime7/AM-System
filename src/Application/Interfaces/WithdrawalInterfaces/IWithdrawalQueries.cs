

using Application.Common.Models;
using Application.Withdrawals.DTOs;
using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.WithdrawalInterfaces
{
    public interface IWithdrawalQueries
    {
        Task<Result<IEnumerable<WithdrawalResponse>>> GetAllWithdrawalsAsync();
        Task<Result<WithdrawalResponse>> GetWithdrawalByIDAsync(int id);
       

    }
}
