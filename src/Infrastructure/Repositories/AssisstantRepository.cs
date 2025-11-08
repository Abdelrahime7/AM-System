using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AssisstantRepository(AppDbContext context) : GenericRepository<Assisstant>(context),IAssisstantRepository
    {
        public override async Task<Assisstant?> GetByIdAsync(int id)
        {
            return await context.Assisstants.Include(x => x.User)
                 .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
