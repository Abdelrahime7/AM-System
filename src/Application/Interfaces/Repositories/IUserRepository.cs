using Application.Admins.Dasboard.DashDto;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IUserRepository :IGenericRepository<User>
    {
        Task<User> GetDriver();
        Task<string> GetRecentUserAsync();
       
    }
}
