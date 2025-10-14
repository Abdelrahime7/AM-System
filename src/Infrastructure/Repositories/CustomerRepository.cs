using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CustomerRepository(AppDbContext context) : GenericRepository<Customer>(context), ICustomerRepository
{
    // Additional methods specific to Program can be added here
    public async Task<Customer?> GetByNameAsync(string name)
    {
        return await context.Customers
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.FullName == name);
    }

    public override async Task AddAsync(Customer entity)
    {
        await context.Customers. AddAsync(entity);

    }

  
}
