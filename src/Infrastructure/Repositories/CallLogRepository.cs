using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class CallLogRepository(AppDbContext context) : GenericRepository<CallLog>(context), ICallLogRepository
{

}
