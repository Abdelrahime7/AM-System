using Application.Interfaces.Common.Mappers;
using Application.Withdrawals.DTOs;
using Domain.Entities;

namespace Application.Withdrawals.Mapper;

public class WithdrawalMapper : IEntityMapper<Withdrawal, CreateWithdrawalRequest,
    UpdateWithdrawalRequest, WithdrawalResponse>
{
    public Withdrawal ToEntity(CreateWithdrawalRequest dto)
    {
        return new Withdrawal
        {
            Amount = dto.Amount,
            Status = dto.Status,
            AffiliateId = dto.AffiliateId,
            AffiliateBalanceId = dto.AffiliateBalanceId,
            ProcessedBy = dto.ProcessedBy
        };
    }

    public WithdrawalResponse ToResponse(Withdrawal entity)
    {
        return new WithdrawalResponse
        {
            Id = entity.Id,
            Amount = entity.Amount,
            Status = entity.Status.ToString(),
            ProcessedAt = entity.ProcessedAt,
            AffiliateId = entity.AffiliateId,
            AffiliateBalanceId = entity.AffiliateBalanceId,
            CurrentBalance = entity.AffiliateBalance.Amount,
            ProcessedBy = entity.ProcessedBy,
        };
    }

    public void ToUpdateEntity(Withdrawal entity, UpdateWithdrawalRequest dto)
    {
        entity.Amount = dto.Amount ?? entity.Amount;
        entity.Status = dto.Status ?? entity.Status;
        entity.ProcessedAt = dto.ProcessedAt ?? entity.ProcessedAt;
        entity.AffiliateId = dto.AffiliateId ?? entity.AffiliateId;
        entity.AffiliateBalanceId = dto.AffiliateBalanceId ?? entity.AffiliateBalanceId;
        entity.ProcessedBy = dto.ProcessedBy ?? entity.ProcessedBy;
    }
}
