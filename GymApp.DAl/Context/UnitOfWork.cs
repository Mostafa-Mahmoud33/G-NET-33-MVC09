using GymApp.DAl.Contracts;
using GymApp.DAl.Repositories;
using System.Collections.Concurrent;

namespace GymApp.DAl.Context
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GymDbContext _dbContext;
        private readonly Dictionary<string, object> _repositories = [];
        public UnitOfWork(GymDbContext dbContext, ISessionRepository sessions)
        {
            _dbContext = dbContext;
            Sessions = sessions;
        }

        public ISessionRepository Sessions { get; set; }



        // public IGenericRepository<Member> Members { get; }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            // Create Repo based on the type of TEntity

            var entityName = typeof(TEntity).Name;

            // Check if the repo is created before
            if(_repositories.ContainsKey(entityName))
                return (_repositories[entityName] as IGenericRepository<TEntity>)!;
            
            var repo = new GenericRepository<TEntity>(_dbContext);

            _repositories.Add(entityName, repo);
            return repo;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => await _dbContext.SaveChangesAsync(cancellationToken);

    }
}
