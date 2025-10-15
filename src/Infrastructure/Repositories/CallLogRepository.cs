using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CallLogRepository(AppDbContext context) : GenericRepository<CallLog>(context), ICallLogRepository
{
    public override async Task<IEnumerable<CallLog>> GetAllAsync()
    {
        return await context. CallLogs.
             Include(c => c.Agent)
             .Include(c => c.Order)
             .ToListAsync();
    }
    public override async Task<CallLog?> GetByIdAsync(int id)
    {
        return await  context.CallLogs.
             Include(c => c.Agent)
             .Include(c => c.Order).FirstOrDefaultAsync(c => c.Id == id);
    }
}
