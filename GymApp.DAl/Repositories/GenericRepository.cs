using GymApp.DAl.Context;
using GymApp.DAl.Contracts;
using System.Linq.Expressions;

namespace GymApp.DAl.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity

    {
        protected readonly GymDbContext _context;

        public GenericRepository(GymDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false, CancellationToken cancellationToken = default)
        {
            return trackChanges
                ? await _context.Set<TEntity>().ToListAsync(cancellationToken)
                : await _context.Set<TEntity>().AsNoTracking().ToListAsync(cancellationToken);
        }

        // Get By Id
        public async Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
            => await _context.Set<TEntity>().FindAsync([id], cancellationToken);

        // Change the Entity State Local
        public async void Add(TEntity entity, CancellationToken cancellationToken = default)
        {
            _context.Set<TEntity>().Add(entity);
            
        }

        // Update
        public async void Update(TEntity entity, CancellationToken cancellationToken = default)
        {
            _context.Set<TEntity>().Update(entity);
            
        }

        // Delete
        public async void Delete(TEntity entity, CancellationToken cancellationToken = default)
        {
            _context.Set<TEntity>().Remove(entity);
            
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
            => _context.Set<TEntity>().AnyAsync(predicate, cancellationToken);

        public Task<TEntity?> FirstOrDefault(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return _context.Set<TEntity>().FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public IQueryable<TEntity> GetQueryable()
        {
            return _context.Set<TEntity>();
        }
    }
}
