using Application.AuditsLog.DTOs;
using Application.CustomizedOrders.DTOs;
using Application.Interfaces.Common.Mappers;
using Application.Withdrawals.DTOs;
using Domain.Entities;


namespace Application.Withdrawals.Mapper;

public class WithdrawalMapper : IEntityMapper<Withdrawal, CreateWithdrawalRequest,
    UpdateWithdrawalRequest, WithdrawalResponse>
{
    public Withdrawal ToEntity(CreateWithdrawalRequest dto)
    {
        throw new NotImplementedException();
    }

    public WithdrawalResponse ToResponse(Withdrawal entity)
    {
        throw new NotImplementedException();
    }

    public void ToUpdateEntity(Withdrawal entity, UpdateWithdrawalRequest dto)
    {
        throw new NotImplementedException();
    }
}