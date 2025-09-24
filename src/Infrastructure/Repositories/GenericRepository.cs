using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {

        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        // The methods are marked as virtual so that we can override them
        // in derived classes if validation or custom logic is needed.

        public virtual async Task<TEntity?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbSet.ToListAsync();
        public virtual async Task AddAsync(TEntity entity) => await _dbSet.AddAsync(entity);
        public virtual void Update(TEntity entity) => _dbSet.Update(entity);
        public virtual void Delete(TEntity entity) => _dbSet.Remove(entity);
      
       
    }
}
