using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TokenRepository(AppDbContext context) : GenericRepository<RefereshToken>(context), ITokenRepository
{
    public async Task<RefereshToken> GetbyValueAsync(string token)
    {
        var result = await context.Tokens.AsNoTracking().
            FirstOrDefaultAsync(x => x.TokenValue == token);
        return  result;
    }
}
