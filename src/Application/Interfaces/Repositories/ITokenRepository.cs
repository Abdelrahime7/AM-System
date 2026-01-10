using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface ITokenRepository : IGenericRepository<RefereshToken>
{
   Task<RefereshToken> GetbyValueAsync(string token);

}