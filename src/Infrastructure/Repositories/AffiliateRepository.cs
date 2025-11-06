using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repositories
{
    public class AffiliateRepository(AppDbContext context) :GenericRepository<Affiliate>(context) ,IAffiliateRepository
    {
        public override  Task<Affiliate?> GetByIdAsync(int id)
        {
          return     context.Affiliates.Include(x=>x.user)
                 .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
