

using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IDriverRepository :IGenericRepository<Driver>
    {
        Task<int> CountPendingAsync(CancellationToken cancellationToken = default); 

    }
}
