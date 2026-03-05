using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repositories
{
    public class AffiliateRepository(AppDbContext context) :GenericRepository<Affiliate>(context) ,IAffiliateRepository
    {
        public Task<int>  CountActiveAsync(CancellationToken cancellationToken = default)
        {
            return context.Affiliates.CountAsync(a => a.user.Status == UserStatus.Active, cancellationToken);
        }

        public Task<int> CountPendingAsync(CancellationToken cancellationToken = default)
        {
            return context.Affiliates.CountAsync(a => a.user.Status == UserStatus.Pending, cancellationToken);
        }

        public override  Task<Affiliate?> GetByIdAsync(int id)
        {
          return     context.Affiliates.Include(x=>x.user)
                 .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
