using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;
namespace Infrastructure.Repositories
{
    public class AdminRepository(AppDbContext context) :GenericRepository<Admin>(context) ,IAdminRepository
    {
    }
}
