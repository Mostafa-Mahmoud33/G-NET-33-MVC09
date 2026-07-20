namespace GymApp.DAl.Contracts
{
    public interface ISessionRepository : IGenericRepository<Session>
    {
        Task<IEnumerable<Session>> GetAllSessionsWithCategoryAndTrainerAsync(CancellationToken cancellationToken = default);
        Task<int> CountOfBookedSlotsAsync(int sessionId, CancellationToken cancellationToken = default);

        Task<Session?> GetSessionWithCategoryAndTrainerByIdAsync(int sessionId, CancellationToken cancellationToken = default);
    }
}
