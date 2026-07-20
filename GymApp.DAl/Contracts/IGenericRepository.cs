using System.Linq.Expressions;
namespace GymApp.DAl.Contracts
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        void Add(TEntity TEntity, CancellationToken cancellationToken = default);
        void Delete(TEntity TEntity, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false, CancellationToken cancellationToken = default);
        Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        void Update(TEntity TEntity, CancellationToken cancellationToken = default);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<TEntity?> FirstOrDefault(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);


        IQueryable<TEntity> GetQueryable();
    }
}
