using Application.Common.Models;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.Repositories;
using Application.Interfaces.WithdrawalInterfaces;
using Application.Withdrawals.DTOs;
using Domain.Entities;

namespace Application.Withdrawals.Features.Queries;

public partial class WithdrawalQueries(
    IWithdrawalRepository repository,
    IEntityMapper<Withdrawal, CreateWithdrawalRequest, UpdateWithdrawalRequest, WithdrawalResponse> mapper)
    : IWithdrawalQueries
{
    private readonly IWithdrawalRepository _repository = repository;
    private readonly IEntityMapper<Withdrawal, CreateWithdrawalRequest, UpdateWithdrawalRequest, WithdrawalResponse> _mapper = mapper;

    public async Task<Result<IEnumerable<WithdrawalResponse>>> GetAllWithdrawalsAsync()
    {
        try
        {
            var withdrawals = await _repository.GetAllAsync();
            if (!withdrawals.Any())
                return Result<IEnumerable<WithdrawalResponse>>.Failure("No Withdrawals Found");

            var response = withdrawals.ToList().Select(c => _mapper.ToResponse(c));
            return Result<IEnumerable<WithdrawalResponse>>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<WithdrawalResponse>>.Failure($"failed to fetch withdrawals: {ex.Message}");
        }
    }
}
