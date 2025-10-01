using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class TokenRepository(AppDbContext context) : GenericRepository<Token>(context), ITokenRepository
{
}
