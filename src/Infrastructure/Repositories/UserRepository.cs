

using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository(AppDbContext context) : GenericRepository<User>(context), IUserRepository
    {
        public async  Task<User> GetDriver()
        {
           
            
            var deiver = await context.Users.
                Where(u=>u.Role.RoleType==UserRole.Driver)
                .FirstOrDefaultAsync();
            return deiver;
         }
    }
}
