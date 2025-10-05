using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class WithdrawalRepository(AppDbContext context) : GenericRepository<Withdrawal>(context), IWithdrawalRepository
{
    public override async Task<IEnumerable<Withdrawal>> GetAllAsync()
    {
        return await _context.Withdrawals
            .AsNoTracking()
            .Include(w => w.Affiliate)
            .Include(w => w.ProcessedByUser)
            .Include(w => w.AffiliateBalance)
            .ToListAsync();
    }

    public override async Task<Withdrawal?> GetByIdAsync(int id)
    {
        return await _context.Withdrawals
            .AsNoTracking()
            .Include(w => w.Affiliate)
            .Include(w => w.ProcessedByUser)
            .Include(w => w.AffiliateBalance)
            .FirstOrDefaultAsync(w => w.Id == id);
    }
}