using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class WithdrawalRepository(AppDbContext context) : GenericRepository<Withdrawal>(context), IWithdrawalRepository
{
   
}
