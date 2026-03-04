

using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IAssisstantRepository:IGenericRepository<Assisstant>
    {
        Task<int> CountPendingAsync(CancellationToken cancellationToken = default);
    }
}
