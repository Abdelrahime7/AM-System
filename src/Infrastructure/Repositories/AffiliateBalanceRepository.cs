using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AffiliateBalanceRepository(AppDbContext context) : GenericRepository<AffiliateBalance>(context), IAffiliateBalanceRepository
{
   public override async Task<AffiliateBalance?> GetByIdAsync(int id)
   {
      return await _context.AffiliateBalances
         .AsNoTracking()
         .Include(u => u.Affiliate)
         .FirstOrDefaultAsync(a => a.Id == id);
   }

   public override async Task<IEnumerable<AffiliateBalance>> GetAllAsync()
   {
      return await _context.AffiliateBalances
         .AsNoTracking()
         .Include(u => u.Affiliate)
         .ToListAsync();
   }
}
