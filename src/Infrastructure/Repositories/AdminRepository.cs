using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AdminRepository(AppDbContext context) :GenericRepository<Admin>(context) ,IAdminRepository
    {
        public Task<int> CountPendingAsync(CancellationToken cancellationToken = default)
        {
            return context.Admins.CountAsync(A=>A.user.Status==UserStatus.Pending);
        }

        public override async Task<Admin?> GetByIdAsync(int id)
        {
            return await context.Admins.Include(x => x.user)
                 .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
