using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductImageRepository(AppDbContext context) : GenericRepository<ProductImage>(context), IProductImageRepository
{
    public override async Task<IEnumerable<ProductImage>> GetAllAsync()
    {
        return await _context.ProductImages
            .AsNoTracking()
            .Include(pi => pi.Product)
            .Include(pi => pi.CustomizedOrder)
            .ToListAsync();
    }

    public override async Task<ProductImage?> GetByIdAsync(int id)
    {
        return await _context.ProductImages
            .AsNoTracking()
            .Include(pi => pi.Product)
            .Include(pi => pi.CustomizedOrder)
            .FirstOrDefaultAsync(pi => pi.Id == id);
    }

    public async Task<IEnumerable<ProductImage>> GetByProductIdAsync(int productId)
    {
        return await _context.ProductImages
            .AsNoTracking()
            .Include(pi => pi.Product)
            .Where(pi => pi.ProductId == productId)
            .OrderByDescending(pi => pi.IsPrimary)
            .ThenBy(pi => pi.Id)
            .ToListAsync();
    }

    public async Task<IEnumerable<ProductImage>> GetByCustomizedOrderIdAsync(int customizedOrderId)
    {
        return await _context.ProductImages
            .AsNoTracking()
            .Include(pi => pi.CustomizedOrder)
            .Where(pi => pi.CustomizedOrderId == customizedOrderId)
            .OrderByDescending(pi => pi.IsPrimary)
            .ThenBy(pi => pi.Id)
            .ToListAsync();
    }

    public async Task<ProductImage?> GetPrimaryImageByProductIdAsync(int productId)
    {
        return await _context.ProductImages
            .AsNoTracking()
            .Include(pi => pi.Product)
            .FirstOrDefaultAsync(pi => pi.ProductId == productId && pi.IsPrimary);
    }

    public async Task<ProductImage?> GetPrimaryImageByCustomizedOrderIdAsync(int customizedOrderId)
    {
        return await _context.ProductImages
            .AsNoTracking()
            .Include(pi => pi.CustomizedOrder)
            .FirstOrDefaultAsync(pi => pi.CustomizedOrderId == customizedOrderId && pi.IsPrimary);
    }

    public async Task<bool> SetPrimaryImageAsync(int imageId)
    {
        var image = await _context.ProductImages.FindAsync(imageId);
        if (image == null) 
            return false;

        // Reset other primary images for the same entity
        IQueryable<ProductImage> imagesToReset;
        if (image.ProductId.HasValue)
        {
            imagesToReset = _context.ProductImages
                .Where(pi => pi.ProductId == image.ProductId && pi.Id != imageId && pi.IsPrimary);
        }
        else if (image.CustomizedOrderId.HasValue)
        {
            imagesToReset = _context.ProductImages
                .Where(pi => pi.CustomizedOrderId == image.CustomizedOrderId && pi.Id != imageId && pi.IsPrimary);
        }
        else
        {
            return false;
        }

        foreach (var img in imagesToReset)
        {
            img.IsPrimary = false;
        }

        await imagesToReset.ExecuteUpdateAsync(s =>
            s.SetProperty(pi => pi.IsPrimary, false));

        image.IsPrimary = true;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> HasAnyPrimaryImageForProductAsync(int productId)
    {
        return await _context.ProductImages
            .AnyAsync(pi => pi.ProductId == productId && pi.IsPrimary);
    }

    public async Task<bool> HasAnyPrimaryImageForCustomizedOrderAsync(int customizedOrderId)
    {
        return await _context.ProductImages
            .AnyAsync(pi => pi.CustomizedOrderId == customizedOrderId && pi.IsPrimary);
    }
}