using Application.Common.Models;
using Application.Delivery.DTOs;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.DeliveryInterfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.WithdrawalInterfaces;
using Application.Withdrawals.DTOs;
using Domain.Entities;

namespace Application.Withdrawals.Features.Commands;

public partial class WithdrawalCommands : IWithdrawalCommands
{
    public Task<Result<int>> CreatWithdrawalAsync(CreateWithdrawalRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> DeleteWithdrawalAsync(int ID)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> UpdateWithdrawalAsync(UpdateWithdrawalRequest request)
    {
        throw new NotImplementedException();
    }
}
