using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class DriverRepository(AppDbContext context)  :GenericRepository<Driver>(context),IDriverRepository
    {
        public async Task<int> CountPendingAsync(CancellationToken cancellationToken = default)

        {
            return await context.Drivers.CountAsync(d => d.User!.Status == UserStatus.Pending, cancellationToken);
        }

        public override async Task<Driver?> GetByIdAsync(int id)
        {
           return await  context.Drivers.Include(x=>x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
