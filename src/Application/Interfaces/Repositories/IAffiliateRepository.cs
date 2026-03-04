using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IAffiliateRepository:IGenericRepository<Affiliate>
    {
        Task<int> CountActiveAsync(CancellationToken cancellationToken= default);
        Task<int> CountPendingAsync(CancellationToken cancellationToken = default);
    }
}
