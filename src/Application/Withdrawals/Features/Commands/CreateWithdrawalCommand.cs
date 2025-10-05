using Application.Common.Models;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.Repositories;
using Application.Interfaces.WithdrawalInterfaces;
using Application.Withdrawals.DTOs;
using Domain.Entities;

namespace Application.Withdrawals.Features.Commands;

public partial class WithdrawalCommands(
    IWithdrawalRepository repository,
    IEntityMapper<Withdrawal, CreateWithdrawalRequest, UpdateWithdrawalRequest, WithdrawalResponse> mapper)
    : IWithdrawalCommands
{
    private readonly IWithdrawalRepository _repository = repository;
    private readonly IEntityMapper<Withdrawal, CreateWithdrawalRequest, UpdateWithdrawalRequest, WithdrawalResponse> _mapper = mapper;

    public async Task<Result<int>> CreateWithdrawalAsync(CreateWithdrawalRequest request)
    {
        try
        {
            var withdrawal = _mapper.ToEntity(request);
            await _repository.AddAsync(withdrawal);
            return Result<int>.Success(withdrawal.Id);
        }
        catch (Exception ex)
        {
            return Result<int>.Failure($"Error creating withdrawal: {ex.Message}");
        }
    }
}