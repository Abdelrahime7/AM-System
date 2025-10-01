using Application.Common.Models;
using Application.Customers.DTOs;
using Application.Delivery.DTOs;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.CustomerInterfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.WithdrawalInterfaces;
using Application.Withdrawals.DTOs;
using Domain.Entities;

namespace Application.Withdrawals.Features.Queries;

public partial class WithdrawalQueries : IWithdrawalQueries
{
    public Task<Result<IEnumerable<WithdrawalResponse>>> GetAllWithdrawalsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Result<WithdrawalResponse>> GetWithdrawalByIDAsync(int id)
    {
        throw new NotImplementedException();
    }
}

}