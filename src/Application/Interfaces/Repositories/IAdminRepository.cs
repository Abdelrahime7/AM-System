using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IAdminRepository:IGenericRepository<Admin>
    {
        Task<int> CountPendingAsync(CancellationToken cancellationToken= default);
    }
}
