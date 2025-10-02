

using Application.Common.Models;
using Application.Withdrawals.DTOs;
using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.WithdrawalInterfaces
{
    public interface IWithdrawalCommands
    {
        Task<Result<int>> CreatWithdrawalAsync(CreateWithdrawalRequest request);
        Task<Result<bool>> DeleteWithdrawalAsync(int ID);
        Task<Result<bool>> UpdateWithdrawalAsync(UpdateWithdrawalRequest request);
      

    }
}
