using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository(AppDbContext context) : GenericRepository<Product>(context), IProductRepository
{
    // Additional methods specific to the entity can be added here
    public override async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products
            .AsNoTracking()
            .Include(u => u.CreatedBy)
            .ToListAsync();
    }   

    public override async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products
            .AsNoTracking()
            .Include(u => u.CreatedBy)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Product?> GetByNameAsync(string name)
    {
        return await _context.Products
            .AsNoTracking()
            .Include(u => u.CreatedBy)
            .FirstOrDefaultAsync(c => c.Name == name);
    }

    public async Task<string> GetRecentProductAsync()
    {
        var lastProuduct = await _context.Products.FirstOrDefaultAsync(P => P.Status == ProductStatus.Active);

        if (lastProuduct != null) {
            return $"Product: {lastProuduct.Description} waiting for review ";

        }
        return "";
    }
}