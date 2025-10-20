using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class DriverRepository(AppDbContext context)  :GenericRepository<Driver>(context),IDriverRepository
    {

    }
}
