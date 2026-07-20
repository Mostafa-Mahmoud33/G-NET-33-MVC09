namespace GymApp.DAl.Contracts
{
    public interface IUnitOfWork
    {
        // public IGenericRepository<Member> Members {  get; }
        public ISessionRepository Sessions { get; set; }

        // Access Each and Every Repo
        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
