using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IWithdrawalRepository : IGenericRepository<Withdrawal>
{
    Task<string> GetRecentWithdrawelAsync();
}