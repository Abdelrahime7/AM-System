using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RoleRepository(AppDbContext context) : GenericRepository<Role>(context), IRoleRepository
{
    public override async Task<IEnumerable<Role>> GetAllAsync()
    {
        return await _context.Roles
            .AsNoTracking()
            .Include(r => r.Users)
            .ToListAsync();
    }

    public async Task<Role?> GetByRoleTypeAsync(UserRole roleType)
    {
        return await _context.Roles
            .AsNoTracking()
            .Include(r => r.Users)
            .FirstOrDefaultAsync(r => r.RoleType == roleType);
    }

    public override async Task<Role?> GetByIdAsync(int id)
    {
        return await _context.Roles
            .AsNoTracking()
            .Include(r => r.Users)
            .FirstOrDefaultAsync(r => r.Id == id);
    }
}
