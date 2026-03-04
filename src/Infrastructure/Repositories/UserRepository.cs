

using Application.Admins.Dasboard.DashDto;
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
           throw new NotImplementedException();
            
            //var deiver = await context.Users.
            //    Where(u=>u.Role.RoleType==UserRole.Driver)
            //    .FirstOrDefaultAsync();
            //return deiver;
         }

        public override async Task AddAsync(User entity)
        {
          await  context.Users.AddAsync(entity);

        }

        public async Task<string> GetRecentUserAsync()
        {
            var User = await context.Users.FirstOrDefaultAsync(U => U.Status == UserStatus.Active);
            if (User != null)
            {
              return $"User {User.FullName} registred as {User.Role}";
            }
              
            return " ";
        }
    }
}
