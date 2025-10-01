using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class AffiliateBalanceRepository(AppDbContext context) : GenericRepository<AffiliateBalance>(context), IAffiliateBalanceRepository
{
   
}
