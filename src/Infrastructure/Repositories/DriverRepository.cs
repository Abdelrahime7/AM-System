using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class DriverRepository(AppDbContext context)  :GenericRepository<Driver>(context),IDriverRepository
    {
        public override async Task<Driver?> GetByIdAsync(int id)
        {
           return await  context.Drivers.Include(x=>x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
