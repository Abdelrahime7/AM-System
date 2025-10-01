using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class RoleRepository(AppDbContext context) : GenericRepository<Role>(context), IRoleRepository
{
}
