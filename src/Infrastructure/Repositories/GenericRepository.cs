using Application.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {

        protected readonly AppDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        // The methods are marked as virtual so that we can override them
        // in derived classes if validation or custom logic is needed.

        public virtual async Task<TEntity?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbSet.ToListAsync();
        public virtual async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);    
            await _context.SaveChangesAsync();
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Update(entity);
           _context.SaveChanges();
        }
        public virtual void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();

        }
        }
}
