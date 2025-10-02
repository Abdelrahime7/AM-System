using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class AuditLogRepository(AppDbContext context) : GenericRepository<AuditLog>(context), IAuditLogRepository
{
    
    
}
