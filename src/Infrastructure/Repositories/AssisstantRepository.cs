using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AssisstantRepository(AppDbContext context) : GenericRepository<Assisstant>(context),IAssisstantRepository
    {
        public async Task<int> CountPendingAsync(CancellationToken cancellationToken = default)
        {
            return await context.Assisstants.CountAsync(A=>A.User.Status==UserStatus.Pending, cancellationToken);
        }

        public override async Task<Assisstant?> GetByIdAsync(int id)
        {
            return await context.Assisstants.Include(x => x.User)
                 .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
