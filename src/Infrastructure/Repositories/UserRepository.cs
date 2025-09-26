

using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class UserRepository(AppDbContext context) : GenericRepository<User>(context) , IUserRepository
    {
       
    }
}
